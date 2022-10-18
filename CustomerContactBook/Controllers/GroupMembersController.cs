using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomerContactBook.Models;
using CustomerContactBook.Controllers;

namespace CustomerContactBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupMembersController : ControllerBase
    {
        private readonly ContactBookContext _context;

        public GroupMembersController(ContactBookContext context)
        {
            _context = context;
        }

        // GET: api/GroupMembers
        /// <summary>
        /// return all group members
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupMember>>> GetGroupMembers()
        {
            return await _context.GroupMembers.ToListAsync();
        }

        // GET: api/GroupMembers/5
        /// <summary>
        /// get groupmember with same id
        /// </summary>
        /// <param name="id">id of group member</param>
        [HttpGet("{Cid}/{Gid}")]
        public async Task<ActionResult<GroupMember>> GetGroupMember(long Cid, long Gid)
        {
            var groupMember = await _context.GroupMembers.FindAsync(Cid, Gid);

            if (groupMember == null)
            {
                return NotFound();
            }

            return groupMember;
        }

        // PUT: api/GroupMembers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// updates groupmember with specified id.
        /// returns 404 not found if id doesnt exits
        /// </summary>
        /// <param name="id">id of group member to make</param>
        /// <param name="groupMember">group member to make</param>
        [HttpPut("{Cid}/{Gid}")]
        public async Task<IActionResult> PutGroupMember(long Gid,long Cid , GroupMember groupMember)
        {
            var toChange = await _context.GroupMembers.FindAsync(Cid, Gid);
            var customer = await _context.Customers.FindAsync(groupMember.CustomerId);
            var group = await _context.Groups.FindAsync(groupMember.GroupId);
            if (customer == null || group == null || toChange == null)
            {
                return BadRequest();
            }

            await DeleteGroupMember(Cid, Gid);
            await PostGroupMember(groupMember);
            return NoContent();
        }

        // POST: api/GroupMembers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// create a group member
        /// </summary>
        /// <param name="groupMember">group member to create</param>
        [HttpPost]
        public async Task<ActionResult<GroupMember>> PostGroupMember(GroupMember groupMember)
        {
            var customer = await _context.Customers.FindAsync(groupMember.CustomerId);
            var group = await _context.Groups.FindAsync(groupMember.GroupId);

            //if the customer and group does not yet exist return NotFound()
            if (customer == null || group == null)
            {
                return NotFound();
            }

            _context.GroupMembers.Add(groupMember);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGroupMember", new { Cid = groupMember.CustomerId, Gid = groupMember.GroupId }, groupMember);
        }

        // DELETE: api/GroupMembers/5
        /// <summary>
        /// delete a group member with specified id
        /// </summary>
        /// <param name="Cid">partial key</param>
        /// <param name="Gid">partial key</param>
        [HttpDelete("{Cid}/{Gid}")]
        public async Task<IActionResult> DeleteGroupMember(long Cid, long Gid)
        {
            var groupMember = await _context.GroupMembers.FindAsync(Cid, Gid);
            if (groupMember == null)
            {
                return NotFound();
            }

            _context.GroupMembers.Remove(groupMember);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
