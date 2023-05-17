namespace API_Web.Model
{
    public class Employee
    {
        public Guid guid { get; set; }
        public char nik { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime birthDate { get; set; }
        public int Gender { get; set; }
        public DateTime hiringDate { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime modifiedDate { get; set; }

    }
}
