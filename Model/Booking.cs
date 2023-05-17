namespace API_Web.Model
{
    public class Booking
    {
        public Guid guid { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int status { get; set; }
        public string remarks { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime modifiedDate { get; set; }
        public Guid roomGuid { get; set; }
        public Guid employeeGuid { get; set; }
    }
}
