namespace Infinigate.Afas.Threats.Classes
{
    public class OAuthResponse {
        private string? _access_token;
        private string? _token_type;
        private string? _expires_in;
        private string? _scope;

        public string? access_token 
        {
            get { return _access_token; }
            set { _access_token = value; }
        }

        public string? token_type 
        {
            get { return _token_type; }
            set { _token_type = value; }
        }

        public string? expires_in 
        {
            get { return _expires_in; }
            set { _expires_in = value; }
        }

        public string? scope 
        {
            get { return _scope; }
            set { _scope = value; }
        }

    }

}