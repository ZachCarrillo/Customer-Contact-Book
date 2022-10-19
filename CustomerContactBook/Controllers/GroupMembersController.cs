using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomerContactBook.Models;
using CustomerContactBook.Controllers;
using CustomerContactBook.Services;

namespace CustomerContactBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupMembersController : ControllerBase
    {
        private readonly MembersService _membersService;

        public GroupMembersController(MembersService membersService)
        {
            _membersService = membersService;
        }

        // GET: api/GroupMembers
        /// <summary>
        /// return all group members
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupMember>>> GetGroupMembers()
        {
            var result = await _membersService.GetGroupMembers();
            return result;
        }

        // GET: api/GroupMembers/5
        /// <summary>
        /// get groupmember with same id
        /// </summary>
        /// <param name="id">id of group member</param>
        [HttpGet("{Cid}/{Gid}")]
        public async Task<ActionResult<GroupMember>> GetGroupMember(long Cid, long Gid)
        {
            var result = await _membersService.GetGroupMember(Cid, Gid);

            return result == null ? NotFound() : result;
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
            var result = await _membersService.PutGroupMember(Cid, Gid, groupMember);
            return result == true ? NoContent() : BadRequest();
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
            var result = await _membersService.PostGroupMember(groupMember);

            return result == null ? NotFound() : CreatedAtAction("GetGroupMember", new { Cid = groupMember.CustomerId, Gid = groupMember.GroupId }, groupMember);
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
            var result = await _membersService.DeleteGroupMember(Cid, Gid);
            return result == false ? NotFound() : NoContent();
        }

    }
}
