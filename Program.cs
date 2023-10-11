//title           :Infinigate.Afas.Sync
//description     :This dotnet app imports the necessary data from Afas
//author          :Wouter Vanbelleghem<wouter.vanbelleghem@infinigate.com>
//date            :08/06/2023
//version         :0.1
//usage           :Infinigate.Afas.Sync or dotnet run in folder
//==============================================================================

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

MySqlConnection conn;
MySqlCommand cmd;
long elapsedMs=0;

string homepath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
string configfile = homepath + "/.Infinigate.Afas.Sync";
var config = Configuration.LoadFromFile(configfile);
var section = config["MySql"];

string mysql_pass = section["mysql_pass"].StringValue;
string mysql_host = section["mysql_host"].StringValue;
string mysql_base = section["mysql_base"].StringValue;
string mysql_user = section["mysql_user"].StringValue;

section = config["Teams"];
string teams_webhook_url = section["webhook_url_sync"].StringValue;

//prereq = date of today, and last run date (read from file somewhere?)
//use Watchguard API on our account to check for threats-number with level > 4 (or configged level) 
//if number greater then zero, download the json of all 
//for each one of them determine customer
//for each one create an incident using - same method as the portal uses? / or do it seperate
//if other method as portal create incident, send a teams webhook; alerting of the new incident



