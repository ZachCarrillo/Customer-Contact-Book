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
        private readonly GroupMemberContext _context;

        public GroupMembersController(GroupMemberContext context)
        {
            _context = context;
        }

        // GET: api/GroupMembers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupMember>>> GetGroupMembers()
        {
            return await _context.GroupMembers.ToListAsync();
        }

        // GET: api/GroupMembers/5
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
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupMemberExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/GroupMembers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GroupMember>> PostGroupMember(GroupMember groupMember)
        {
            _context.GroupMembers.Add(groupMember);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGroupMember", new { id = groupMember.GroupId }, groupMember);
        }

        // DELETE: api/GroupMembers/5
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
