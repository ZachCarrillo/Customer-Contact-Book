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
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupMember>> GetGroupMember(int id)
        {
            var groupMember = await _context.GroupMembers.FindAsync(id);

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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroupMember(int id, GroupMember groupMember)
        {
            if (id != groupMember.GroupId)
            {
                return BadRequest();
            }

            _context.Entry(groupMember).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when(!GroupMemberExists(id))
            {
                return NotFound();
            }

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
            _context.GroupMembers.Add(groupMember);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGroupMember", new { id = groupMember.GroupId }, groupMember);
        }

        // DELETE: api/GroupMembers/5
        /// <summary>
        /// delete a group member with specified id
        /// </summary>
        /// <param name="id">id of group member to delete</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroupMember(int id)
        {
            var groupMember = await _context.GroupMembers.FindAsync(id);
            if (groupMember == null)
            {
                return NotFound();
            }

            _context.GroupMembers.Remove(groupMember);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GroupMemberExists(int id)
        {
            return _context.GroupMembers.Any(e => e.GroupId == id);
        }
    }
}
