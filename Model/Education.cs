namespace API_Web.Model
{
    public class Education
    {
        public Guid guid { get; set; }
        public string major { get; set; }
        public string degree { get; set; }
        public float gpa { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime modifiedDate { get; set; }
        public Guid universityGuid { get; set; }
    }
}
