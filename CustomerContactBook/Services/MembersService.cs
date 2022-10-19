using CustomerContactBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomerContactBook.Services
{
    public class MembersService
    {
        private readonly ContactBookContext _context;

        public MembersService(ContactBookContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<GroupMember>>> GetGroupMembers()
        {
            return await _context.GroupMembers.ToListAsync();
        }

        public async Task<ActionResult<GroupMember>> GetGroupMember(long Cid, long Gid)
        {
            var groupMember = await _context.GroupMembers.FindAsync(Cid, Gid);
            return groupMember;
        }

        public async Task<bool> PutGroupMember(long Gid, long Cid, GroupMember groupMember)
        {
            var toChange = await _context.GroupMembers.FindAsync(Cid, Gid);
            var customer = await _context.Customers.FindAsync(groupMember.CustomerId);
            var group = await _context.Groups.FindAsync(groupMember.GroupId);
            if (customer == null || group == null || toChange == null)
            {
                return false;
            }

            await DeleteGroupMember(Cid, Gid);
            await PostGroupMember(groupMember);
            return true;
        }

        public async Task<GroupMember> PostGroupMember(GroupMember groupMember)
        {
            var customer = await _context.Customers.FindAsync(groupMember.CustomerId);
            var group = await _context.Groups.FindAsync(groupMember.GroupId);

            //if the customer and group does not yet exist return NotFound()
            if (customer == null || group == null)
            {
                return null;
            }

            _context.GroupMembers.Add(groupMember);
            await _context.SaveChangesAsync();

            return groupMember;
        }

        public async Task<bool> DeleteGroupMember(long Cid, long Gid)
        {
            var groupMember = await _context.GroupMembers.FindAsync(Cid, Gid);
            if (groupMember == null)
            {
                return false;
            }

            _context.GroupMembers.Remove(groupMember);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
