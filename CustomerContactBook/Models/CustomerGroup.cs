using System.ComponentModel.DataAnnotations;

namespace CustomerContactBook.Models
{
    /// <summary>
    /// Model Class for Customer Group
    /// </summary>
    public class CustomerGroup
    {
        /// <summary>
        /// identifier for CustomerGroup
        /// </summary>
        /// <example>7032</example>
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// Group Name
        /// </summary>
        /// <example>Students</example>
        public string Name { get; set; }
    }
}
