namespace Infinigate.Afas.Threats.Classes
{
    public class AccountItem
    {
        public string? accountId { get; set; }
        public List<AccountChildren>? childrenList { get; set; }
        public string? name { get; set; }
        public bool? parentAccess { get; set; }
        public string? relationName { get; set; }
        public string? relationType { get; set; }
        public int? totalOperators { get; set; }
        public int? type { get; set; }
    }

}