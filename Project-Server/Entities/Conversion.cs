namespace Project_Server.Entities
{
    public class Conversion
    {        public DateTime Time_last_update_utc { get; set; }
        public DateTime Time_next_update_utc { get; set; }
        public string Base_code { get; set; }
        public Dictionary<string, double> Conversion_rates { get; set; } = new Dictionary<string, double>();
    }
}
