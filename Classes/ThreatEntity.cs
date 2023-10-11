namespace Infinigate.Afas.Threats.Classes
{
    public class ThreatEntity {
        
        public string type { get; set; }
        public string name { get; set; }
        public List<RecommendedAction> recommendedActions { get; set; }
        public ThreatDetails threatDetails { get; set; }
    }

}