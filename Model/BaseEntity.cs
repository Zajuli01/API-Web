namespace API_Web.Model
{
    public abstract class BaseEntity
    {
        public Guid Guid { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
