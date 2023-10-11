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

        public static string Base64Encode(string plainText) 
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string GetOrganisation(string? WatchguardAccount, MySqlConnection conn) {            
            Organisation org = new();
            MySqlCommand? cmd;
            MySqlDataReader? reader;

            if (WatchguardAccount != null) {
                string sql = "SELECT organisationid,name FROM incidents.watchguardaccount_organisation LEFT JOIN incidents.organisations ON organisation=organisationid WHERE watchguardaccount='" + WatchguardAccount + "'";
                //try {
                    cmd = new(sql,conn);
                    reader = cmd.ExecuteReader();

                    if (reader.HasRows) {                        
                        reader.Read();   
                        
                        org = new((IDataRecord)reader);
                        
                        reader.Close();
                    }                    
                //} catch (Exception ex) {
                //    Console.WriteLine("GetOrganisation  : " + ex.Message);
                //} finally {
                //    reader = null;
                //    cmd = null;
                //}
            }            
            
            return org.ToString();
        }
    }
}