using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace CustomerContactBook.Database.Tables
{
    /// <summary>
    /// Model Class for GroupMember
    /// </summary>
    public class GroupMember
    {
        /// <summary>
        /// id for customer in a group
        /// </summary>
        public long CustomerId { get; set; }
        /// <summary>
        /// id for group the customer is in
        /// </summary>
        public long GroupId { get; set; }
    }
}
