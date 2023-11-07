namespace Scriptingo.Admin.Models
{
    public class ProcessModel
    {
        public long? ID { get; set; }
        public string name { get; set; }

        public string? description { get; set; }

        public string? sql { get; set; }

        public int con_id { get; set; }

        public bool active { get; set; }

        public string? request { get; set; }

        public string? response { get; set; }
    }
}
