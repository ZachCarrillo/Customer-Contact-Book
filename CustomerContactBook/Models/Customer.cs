namespace CustomerContactBook.Models
{
    /// <summary>
    ///  Model class for Customer
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Customer identifier
        /// </summary>
        /// <example>1893</example>
        public long Id { get; set; }
        /// <summary>
        /// Customer first name
        /// </summary>
        /// <example>Zach</example>
        public string? FirstName { get; set; }
        /// <summary>
        /// Customer Last Name
        /// </summary>
        /// <example>Carrillo</example>
        public string? LastName { get; set; }
        /// <summary>
        /// Customer Full Name
        /// </summary>
        /// <example>Carrillo, Zach</example>
        public string FullName => LastName + ", " + FirstName;
        /// <summary>
        /// Customer phone number
        /// </summary>
        /// <example>+17329830123</example>
        public string? PhoneNum { get; set; }
        /// <summary>
        /// Customer email
        /// </summary>
        /// <example>johnSmith123@gmail.com</example>
        public string? Email { get; set; }
        /// <summary>
        /// Customer address
        /// </summary>
        /// <example>1600 Pennsylvania Ave</example>
        public string? Address { get; set; }
        /// <summary>
        /// Customer birthday
        /// </summary>
        /// <example>1/20/1987</example>
        public DateTime? BirthDay { get; set; }
    }
}
