using CustomerContactBook.Database;
using CustomerContactBook.Database.Tables;
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

        public async Task<List<GroupMemberModel>> GetGroupMembers()
        {
            var groupMembers =  await _context.GroupMembers.ToListAsync();
            return groupMembers.Select(ToGroupMemberModel).ToList();
        }

        public async Task<GroupMemberModel> GetGroupMember(long Cid, long Gid)
        {
            var groupMember = await _context.GroupMembers.FindAsync(Cid, Gid);
            if (groupMember == null)
            {
                return null;
            }
            return ToGroupMemberModel(groupMember);
        }

        public async Task<bool> UpdateGroupMember(long Cid, long Gid, GroupMemberModel groupMember)
        {
            var toChange = await _context.GroupMembers.FindAsync(Cid, Gid);
            var customer = await _context.Customers.FindAsync(groupMember.CustomerId);
            var group = await _context.Groups.FindAsync(groupMember.GroupId);
            if (customer == null || group == null || toChange == null)
            {
                return false;
            }

            await DeleteGroupMember(Cid, Gid);
            await CreateGroupMember(groupMember);
            return true;
        }

        public async Task<GroupMemberModel> CreateGroupMember(GroupMemberModel model)
        {
            var customer = await _context.Customers.FindAsync(model.CustomerId);
            var group = await _context.Groups.FindAsync(model.GroupId);

            //if the customer and group does not yet exist return NotFound()
            if (customer == null || group == null)
            {
                return null;
            }
            var groupMember = new GroupMember {
                CustomerId = model.CustomerId,
                GroupId = model.GroupId,
            };
            _context.GroupMembers.Add(groupMember);
            await _context.SaveChangesAsync();

            return model;
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
        private static GroupMemberModel ToGroupMemberModel(GroupMember groupMember)
        {
            return new GroupMemberModel
            {
                CustomerId = groupMember.CustomerId,
                GroupId = groupMember.GroupId,
            };
        }
    }
}
