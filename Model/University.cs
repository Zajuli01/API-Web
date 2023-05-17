namespace API_Web.Model
{
    public class University
    {
        public Guid guid { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime modifiedDate { get; set; }

    }
}
