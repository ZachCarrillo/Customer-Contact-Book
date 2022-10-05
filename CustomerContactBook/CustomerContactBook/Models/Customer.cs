namespace CustomerContactBook.Models
{
    public class Customer
    {
        public long Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string FullName => LastName + ", " + FirstName;
        public string? PhoneNum { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public DateTime? BirthDay { get; set; }
    }
}
