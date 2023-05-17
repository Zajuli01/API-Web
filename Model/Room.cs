namespace API_Web.Model
{
    public class Room
    {
        public Guid guid { get; set; }
        public string name { get; set; }
        public int floor { get; set; }
        public int capacity { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime modifiedDate { get; set; }

    }
}
