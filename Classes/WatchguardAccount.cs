using System;
using System.Data;

namespace Infinigate.Afas.Threats.Classes
{
    public class WatchguardAccount
    {
        private int _organisation = 0;
        public int Organisation {
            get {
                return _organisation;
            }
            set {
                _organisation=value;
                Changed();
            }
        }

        private string _watchguardaccount="";
        public string Account {
            get {
                return _watchguardaccount;
            }
            set {
                _watchguardaccount=value;
                Changed();
            }
        }

        private string _name="";
        public string Name {
            get {
                return _name;
            }
            set {
                _name=value;
                Changed();
            }
        }

        private string _type="";
        public string Type {
            get {
                return _type;
            }
            set {
                _type=value;
                Changed();
            }
        }

        private string _topwatchguardaccount="";
        public string TopAccount {
            get {
                return _topwatchguardaccount;
            }
            set {
                _topwatchguardaccount=value;
                Changed();
            }
        }

        private int _toporganisation = 0;
        public int TopOrganisation {
            get {
                return _toporganisation;
            }
            set {
                _toporganisation=value;
                Changed();
            }
        }

        private bool _haschanged = false;
        public bool Haschanged {
            get {
                return _haschanged;
            }
            set {
                _haschanged=value;
            }
        }

        private void Changed() {
            _haschanged=true;
        }

        public WatchguardAccount() {
            _haschanged=false;
        }

        public WatchguardAccount(IDataRecord row) {
            if (row != null) {
                _organisation = Functions.GetInt(row[1]);
                _watchguardaccount = Functions.GetString(row[2]);
                _name = Functions.GetString(row[3]);
                _type = Functions.GetString(row[4]);
                _topwatchguardaccount = Functions.GetString(row[5]);
                _toporganisation = Functions.GetInt(row[6]);
            }
            _haschanged=false;
        }

        public string ToJSON() {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}