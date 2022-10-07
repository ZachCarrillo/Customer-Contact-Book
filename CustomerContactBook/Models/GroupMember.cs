using Microsoft.Build.Framework;

namespace CustomerContactBook.Models
{
    public class GroupMember
    {
        public long CustomerId { get; set; }
        public long GroupId { get; set; }
    }
}
