namespace Scriptingo.Admin.Models
{
    public class ApiModel
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public bool Active { get; set; }

        public string Host { get; set; }

        public bool IsPrivate{ get; set; }
        public long ApiCode{ get; set; }

    }
}
