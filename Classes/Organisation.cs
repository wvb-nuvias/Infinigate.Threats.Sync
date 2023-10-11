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

            _haschanged=false;
        }

        public Organisation(IDataRecord row) {
            if (row != null) {
                _organisationid = Functions.GetInt(row[0]);
                _name = Functions.GetString(row[1]);
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