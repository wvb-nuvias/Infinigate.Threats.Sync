//title           :Infinigate.Threats.Sync
//description     :This dotnet app Syncs threats from Watchguard Cloud and creates incidents when necessary
//author          :Wouter Vanbelleghem<wouter.vanbelleghem@infinigate.com>
//date            :11/10/2023
//version         :0.1
//usage           :Infinigate.Threats.Sync or dotnet run in folder
//==============================================================================

using System.Security.AccessControl;
using System.Runtime.Serialization;
using System.Net.WebSockets;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Net.Http;
using MySql.Data;
using MySql.Data.MySqlClient;
using SharpConfig;
using TeamsHook.NET;
using Newtonsoft.Json;
using Infinigate.Afas.Threats.Classes;

MySqlConnection conn;
long elapsedMs=0;

string homepath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
string configfile = homepath + "/.Infinigate.Threats.Sync";
var config = Configuration.LoadFromFile(configfile);

var section = config["General"];
string cache_path = section["cache_path"].StringValue + "/watchguard_threats_cache";
string lastrun_path = section["cache_path"].StringValue + "/watchguard_threats_lastrun";
string min_threat_level = section["min_threat_level"].StringValue;

section = config["MySql"];
string mysql_pass = section["mysql_pass"].StringValue;
string mysql_host = section["mysql_host"].StringValue;
string mysql_base = section["mysql_base"].StringValue;
string mysql_user = section["mysql_user"].StringValue;

section = config["Teams"];
string teams_webhook_url = section["webhook_url_threats"].StringValue;

section = config["Watchguard"];
string api_user =  section["api_user"].StringValue;
string api_pass =  section["api_pass"].StringValue;
string api_account =  section["api_account"].StringValue;
string api_auth =  section["api_auth"].StringValue;
string api_base =  section["api_base"].StringValue;
string api_key =   section["api_key"].StringValue;
string api_scope = section["api_scope"].StringValue;

var watch = System.Diagnostics.Stopwatch.StartNew();

string connstring = "Server=" + mysql_host + ";Database=" + mysql_base + ";Uid=" + mysql_user + ";Pwd=" + mysql_pass + ";SslMode=none;convert zero datetime=True";

Console.WriteLine("Connecting to MySql...");
try {
    conn = new MySqlConnection(connstring);
    conn.Open();
} catch (Exception ex) {
    Console.WriteLine("MySql Connect Exception: " + ex.Message);
    return;
}

string? startAfter = "";
string? lastrun = "";
if (System.IO.File.Exists(lastrun_path)) {
    lastrun = System.IO.File.ReadAllText(lastrun_path);    
    startAfter = "&startAfter=" + lastrun;
}

Console.WriteLine("Get Watchguard API Token...");
var client = new HttpClient();
var request = new HttpRequestMessage(HttpMethod.Post, api_auth);

var base64=Functions.Base64Encode(api_user + ":" + api_pass);
request.Headers.Add("Authorization", "Basic " + base64);
var collection = new List<KeyValuePair<string, string>>();
collection.Add(new("grant_type", "client_credentials"));
collection.Add(new("scope", api_scope));
var content = new FormUrlEncodedContent(collection);
request.Content = content;
var response = await client.SendAsync(request);
response.EnsureSuccessStatusCode();
var json_token=await response.Content.ReadAsStringAsync();

if (json_token != null) {
    OAuthResponse? o = JsonConvert.DeserializeObject<OAuthResponse>(json_token);

    if (o != null) {
        //first pull accounts via accounts Api and fill the database table - link with organisation will probably be impossible




        Console.WriteLine("Parsing Threat Incidents...");

        request = new HttpRequestMessage(HttpMethod.Get, api_base + "threatsync/management/v1/" + api_account + "/incidents?tenants=true&sortBy=timestamp&query=threatScore:>" + min_threat_level + startAfter);
        request.Headers.Add("Accept", "application/json");
        request.Headers.Add("Watchguard-Api-Key", api_key);
        request.Headers.Add("Authorization", "Bearer " + o.access_token);
        response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var json_reponse=await response.Content.ReadAsStringAsync();

        ThreatsResponse? c = JsonConvert.DeserializeObject<ThreatsResponse>(json_reponse);

        if (c != null) {        
            Console.WriteLine("" + c.count + " Incidents found." );

            if (c.count > 0) {
                if (c.items != null) {
                    Organisation? org = null;
                    string tmp = "" ;

                    foreach (ThreatItem item in c.items) {
                        org = Functions.GetOrganisation(item.account,conn);                        

                        if (item.entities != null) {
                            foreach (KeyValuePair<string, ThreatEntity> entry in item.entities) {
                                ThreatEntity entity = entry.Value;
                                entity.id = entry.Key;
                                tmp = org.Name + " - " + entity.type + " - " + entity.id;

                                if (entity.type == "firebox") tmp += " - " + entity.name;

                                tmp += " [" + org.WatchguardAccount + " - " + org.WatchguardAccountName;
                                if (!org.WatchguardAccountLink) {
                                    tmp += ": " + item.account;
                                }
                                tmp += "]";

                                Console.WriteLine(tmp);  
                            }
                            //for each one create an incident using - same method as the portal uses? / or do it seperate
                            //if other method as portal create incident, send a teams webhook; alerting of the new incident
                        }

                        lastrun=item.timestamp;
                        //break;
                    }    
                }
            }
        }
    }
}

conn.Close();

System.IO.File.WriteAllText(lastrun_path,lastrun);

watch.Stop();
elapsedMs = watch.ElapsedMilliseconds;
TimeSpan t = TimeSpan.FromMilliseconds(elapsedMs);

Console.WriteLine("Done Syncronizing.  MySql Connection Closed.");
Console.WriteLine("In Total, it took " + t.ToString(@"hh\:mm\:ss\:fff"));

bool sendTeams = false;

if (sendTeams) {
    var tclient = new TeamsHookClient();
    var card = new MessageCard();
    card.Title="Threats Sync to Beneluxportal done.";
    card.Text="Threats are synced, incidents created where necessary.\n It took " + t.ToString(@"hh\:mm\:ss\:fff");
    await tclient.PostAsync(teams_webhook_url, card);
}
