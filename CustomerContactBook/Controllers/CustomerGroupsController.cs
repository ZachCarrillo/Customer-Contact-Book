using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomerContactBook.Services;
using CustomerContactBook.Database.Tables;
using CustomerContactBook.Models;

namespace CustomerContactBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerGroupsController : ControllerBase
    {
        private readonly GroupService _groupService;

        public CustomerGroupsController(GroupService groupService)
        {
            _groupService = groupService;
        }


        /// <summary>
        /// Returns all CustomerGroups
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<CustomerGroupModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CustomerGroupModel>>> GetGroups()
        {
            var result = await _groupService.GetGroups();
            return result;
        }


        /// <summary>
        /// Return customer groups with same id
        /// </summary>
        /// <param name="id"> group id</param>
        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(CustomerGroupModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerGroupModel>> GetCustomerGroup(long id)
        {
            var result = await _groupService.GetCustomerGroup(id);

            return result == null ? NotFound() : result;
        }



        /// <summary>
        /// creates customer group with specified id
        /// returns 404 not found if id doesnt exist
        /// </summary>
        /// <param name="id">id of group</param>
        /// <param name="customerGroup">customerGroup to add</param>
        [HttpPut("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutCustomerGroup(long id, CustomerGroupModel customerGroup)
        {
            if (id != customerGroup.Id)
            {
                return BadRequest();
            }

            var result = await _groupService.UpdateCustomerGroup(id, customerGroup);

            return result == false ? NotFound() : NoContent();
        }


        /// <summary>
        /// Create new customer groip
        /// </summary>
        /// <param name="customerGroup">CustomerGroup to add</param>
        [HttpPost]
        [ProducesResponseType(typeof(CustomerGroupModel), StatusCodes.Status201Created)]
        public async Task<ActionResult<CustomerGroupModel>> PostCustomerGroup(CustomerGroupModel customerGroup)
        {
            var result = await _groupService.CreateCustomerGroup(customerGroup);

            return CreatedAtAction("GetCustomerGroup", new { id = result.Id }, result);
        }


        /// <summary>
        /// Delete specified group
        /// </summary>
        /// <param name="id">id of group to be deleted</param>
        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCustomerGroup(long id)
        {
            var result = await _groupService.DeleteCustomerGroup(id);

            return result == false ? NotFound() : NoContent();
        }
    }
}
