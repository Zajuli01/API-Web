namespace API_Web.Model
{
    public class AccountRole
    {
        public Guid guid { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime modifiedDate { get; set; }
        public Guid accountGuid { get; set; }
        public Guid roleGuid { get; set; }
    }
}
