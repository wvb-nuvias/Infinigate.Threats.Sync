using System;
using System.Data;

namespace Infinigate.Afas.Threats.Classes
{
    public class Organisation
    {
        private int _organisationid = 0;
        public int Organisationid {
            get {
                return _organisationid;
            }
            set {
                _organisationid=value;
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

        private string _address1="";
        public string Address1 {
            get {
                return _address1;
            }
            set {
                _address1=value;
                Changed();
            }
        }

        private string _address2="";
        public string Address2 {
            get {
                return _address2;
            }
            set {
                _address2=value;
                Changed();
            }
        }

        private string _address3="";
        public string Address3 {
            get {
                return _address3;
            }
            set {
                _address3=value;
                Changed();
            }
        }

        private DateTime _created_at=default!;
        public DateTime Created_at {
            get {
                return _created_at;
            }
            set {
                _created_at=value;
                Changed();
            }
        }

        private DateTime _updated_at=default!;        
        public DateTime Updated_at {
            get {
                return _updated_at;
            }
            set {
                _updated_at=value;
                Changed();
            }
        }

        private string _watchguardaccount="";
        public string WatchguardAccount {
            get {
                return _watchguardaccount;
            }
            set {
                _watchguardaccount=value;
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

        public Organisation() {
            _organisationid = 1000051;
            _name = "Nuvias BV (BE)";
            _address1 = "";
            _address2 = "";
            _address3 = "";
            _created_at = DateTime.Now;
            _updated_at = DateTime.Now;
            _haschanged=false;
            _watchguardaccount = "";
        }

        public Organisation(IDataRecord row) {
            if (row != null) {
                _organisationid = Functions.GetInt(row[0]);
                _name = Functions.GetString(row[1]);
                _address1 = Functions.GetString(row[2]);
                _address2 = Functions.GetString(row[3]);
                _address3 = Functions.GetString(row[4]);
                _created_at = Functions.GetDateTime(row[5]);
                _updated_at = Functions.GetDateTime(row[6]);
                _watchguardaccount = Functions.GetString(row[7]);
            }
            _haschanged=false;
        }

        public string ToJSON() {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public override string ToString() {
            return _name + " (" + _organisationid + ")";
        }
    }
}