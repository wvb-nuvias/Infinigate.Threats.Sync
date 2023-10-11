namespace Infinigate.Afas.Threats.Classes
{
    public class ThreatItem
    {
        public string id { get; set; }
        public string account { get; set; }
        public string category { get; set; }
        public string type { get; set; }
        public DateTime timestamp { get; set; }
        public int threatScore { get; set; }
        public string severity { get; set; }
        public string description { get; set; }
        public bool remediated { get; set; }
        public List<Entity> entities { get; set; }
        public List<string> entityIds { get; set; }
        public string status { get; set; }
        public DateTime lastUpdateTime { get; set; }
        public string updatedBy { get; set; }
    }
}