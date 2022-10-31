using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomerContactBook.Controllers;
using CustomerContactBook.Services;
using CustomerContactBook.Database.Tables;
using CustomerContactBook.Models;
using System.Text.RegularExpressions;

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


        /// <summary>
        /// return all group members
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<GroupMemberModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<GroupMemberModel>>> GetGroupMembers()
        {
            var result =  await _membersService.GetGroupMembers();
            return result;
        }


        /// <summary>
        /// get groupmember with same id
        /// </summary>
        /// <param name="id">id of group member</param>
        [HttpGet("{CustomerId:long}/{GroupId:long}")]
        [ProducesResponseType(typeof(GroupMemberModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GroupMemberModel>> GetGroupMember(long CustomerId, long GroupId)
        {
            var result = await _membersService.GetGroupMember(CustomerId, GroupId);

            return result == null ? NotFound() : result;
        }



        /// <summary>
        /// updates groupmember with specified id.
        /// returns 404 not found if id doesnt exits
        /// </summary>
        /// <param name="id">id of group member to make</param>
        /// <param name="groupMember">group member to make</param>
        [HttpPut("{CustomerId:long}/{GroupId:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutGroupMember(long CustomerId, long GroupId, GroupMemberModel groupMember)
        {
            var result = await _membersService.UpdateGroupMember(CustomerId, GroupId , groupMember);
            return result == true ? NoContent() : BadRequest();
        }


        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// create a group member
        /// </summary>
        /// <param name="groupMember">group member to create</param>
        [HttpPost]
        [ProducesResponseType(typeof(GroupMemberModel), StatusCodes.Status201Created)]
        public async Task<ActionResult<GroupMemberModel>> PostGroupMember(GroupMemberModel groupMember)
        {
            var result = await _membersService.CreateGroupMember(groupMember);

            return result == null ? NotFound() : CreatedAtAction("GetGroupMember", new { Cid = groupMember.CustomerId, Gid = groupMember.GroupId }, groupMember);
        }


        /// <summary>
        /// delete a group member with specified id
        /// </summary>
        /// <param name="Cid">partial key</param>
        /// <param name="Gid">partial key</param>
        [HttpDelete("{CustomerId:long}/{GroupId:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteGroupMember(long CustomerId, long GroupId)
        {
            var result = await _membersService.DeleteGroupMember(CustomerId, GroupId);
            return result == false ? NotFound() : NoContent();
        }

    }
}
