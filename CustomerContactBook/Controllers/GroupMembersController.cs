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
        /// <summary>
        /// return all group members
        /// </summary>
        [HttpGet]
<<<<<<< Updated upstream
        public async Task<ActionResult<IEnumerable<GroupMember>>> GetGroupMembers()
=======
        [ProducesResponseType(typeof(List<GroupMemberModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<GroupMemberModel>>> GetGroupMembers()
>>>>>>> Stashed changes
        {
            return await _context.GroupMembers.ToListAsync();
        }

        // GET: api/GroupMembers/5
        /// <summary>
        /// get groupmember with same id
        /// </summary>
        /// <param name="id">id of group member</param>
<<<<<<< Updated upstream
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupMember>> GetGroupMember(int id)
=======
        [HttpGet("{Cid}/{Gid}")]
        [ProducesResponseType(typeof(GroupMemberModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GroupMemberModel>> GetGroupMember(long Cid, long Gid)
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
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
=======
        [HttpPut("{Cid}/{Gid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutGroupMember(long Gid,long Cid , GroupMemberModel groupMember)
        {
            var result = await _membersService.UpdateGroupMember(Cid, Gid, groupMember);
            return result == true ? NoContent() : BadRequest();
>>>>>>> Stashed changes
        }

        // POST: api/GroupMembers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// create a group member
        /// </summary>
        /// <param name="groupMember">group member to create</param>
        [HttpPost]
<<<<<<< Updated upstream
        public async Task<ActionResult<GroupMember>> PostGroupMember(GroupMember groupMember)
        {
            _context.GroupMembers.Add(groupMember);
            await _context.SaveChangesAsync();
=======
        [ProducesResponseType(typeof(GroupMemberModel), StatusCodes.Status201Created)]
        public async Task<ActionResult<GroupMemberModel>> PostGroupMember(GroupMemberModel groupMember)
        {
            var result = await _membersService.CreateGroupMember(groupMember);
>>>>>>> Stashed changes

            return CreatedAtAction("GetGroupMember", new { id = groupMember.GroupId }, groupMember);
        }

        // DELETE: api/GroupMembers/5
        /// <summary>
        /// delete a group member with specified id
        /// </summary>
<<<<<<< Updated upstream
        /// <param name="id">id of group member to delete</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroupMember(int id)
=======
        /// <param name="Cid">partial key</param>
        /// <param name="Gid">partial key</param>
        [HttpDelete("{Cid}/{Gid}")]

        public async Task<IActionResult> DeleteGroupMember(long Cid, long Gid)
>>>>>>> Stashed changes
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
