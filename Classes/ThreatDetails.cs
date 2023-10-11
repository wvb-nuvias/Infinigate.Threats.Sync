namespace Infinigate.Afas.Threats.Classes
{
    public class ThreatDetails
    {
        public string? path { get; set; }
        public string? proxyAction { get; set; }
        public string? md5 { get; set; }
        public string? protocol { get; set; }
        public string? dstAddr { get; set; }
        public string? dstInterface { get; set; }
        public string? maliciousIp { get; set; }
        public string? srcAddr { get; set; }
        public string? policy { get; set; }
        public string? srcInterface { get; set; }
        public int? srcPort { get; set; }
        public DateTime? connectionTime { get; set; }
        public string? location { get; set; }
        public int? dstPort { get; set; }
        public string? malware { get; set; }
        public string? file { get; set; }
        public string? exploit { get; set; }
    }

}