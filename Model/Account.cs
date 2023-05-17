namespace API_Web.Model
{
    public class Account
    {
        public Guid guid { get; set; }
        public string password { get; set; }
        public bool isDeleted { get; set; }
        public int otp { get; set; }
        public bool isUsed { get; set; }
        public DateTime expiredTime { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime modifiedDate { get; set; }
    }
}
