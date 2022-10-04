using Microsoft.Build.Framework;

namespace CustomerContactBook.Models
{
    public class GroupMember
    {
        public int CustomerId { get; set; }
        public int GroupId { get; set; }
    }
}
