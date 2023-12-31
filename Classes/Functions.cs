using System.Runtime.CompilerServices;
using System;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Infinigate.Afas.Threats.Classes
{
    public class Functions
    {
        public static int GetInt(object? obj) {
            int tmp = 0;
            if (obj != null) {
                string? stmp= obj.ToString();
                if (stmp != null) {
                    tmp = int.Parse(stmp);
                }                
            }
            return tmp;
        }

        public static string GetString(object? obj) {
            string tmp = "";
            if (obj != null) {
                string? stmp= obj.ToString();
                if (stmp != null) {
                    tmp = stmp;
                }                
            }
            return tmp;
        }

        public static DateTime GetDateTime(object? obj) {
            DateTime tmp=DateTime.Now;
            if (obj != null) {
                string? stmp= obj.ToString();
                if (stmp != null) {
                    tmp = DateTime.Parse(stmp);
                }                
            }
            return tmp;
        }

        public static string Base64Encode(string plainText) 
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static Organisation GetOrganisation(string? WatchguardAccount, MySqlConnection conn) {            
            Organisation org = new();
            MySqlCommand? cmd;
            MySqlDataReader? reader;

            if (WatchguardAccount != null) {
                string sql = "SELECT organisationid,organisations.name,address1,address2,address3,created_at,updated_at,watchguardaccount,watchguardaccount_organisation.name as watchguardaccountname FROM incidents.watchguardaccount_organisation LEFT JOIN incidents.organisations ON organisation=organisationid WHERE watchguardaccount='" + WatchguardAccount + "'";
                
                cmd = new(sql,conn);
                reader = cmd.ExecuteReader();

                if (reader.HasRows) {                        
                    reader.Read();   
                    
                    org = new((IDataRecord)reader);                    
                }

                reader.Close();
            }            
            
            return org;
        }

        public static List<WatchguardAccount> GetWatchguardAccounts(string? WatchguardAccount, MySqlConnection conn) {
            List<WatchguardAccount> wgaccounts=new();
            WatchguardAccount wgaccount = new();
            MySqlCommand? cmd;
            MySqlDataReader? reader;

            if (WatchguardAccount != null) {
                string sql = "SELECT * FROM watchguardaccount_organisation ORDER BY name";
                
                cmd = new(sql,conn);
                reader = cmd.ExecuteReader();

                if (reader.HasRows) {                        
                    while (reader.Read()) {
                        wgaccount = new((IDataRecord)reader);
                        wgaccounts.Add(wgaccount);
                    }              
                }

                reader.Close();
            }      

            return wgaccounts;
        }
    }
}