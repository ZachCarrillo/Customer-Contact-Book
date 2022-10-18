using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomerContactBook.Models;
using CustomerContactBook.Services;

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

        // GET: api/CustomerGroups
        /// <summary>
        /// Returns all CustomerGroups
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerGroup>>> GetGroups()
        {
            var result = await _groupService.GetGroups();
            return result;
        }

        // GET: api/CustomerGroups/5
        /// <summary>
        /// Return customer groups with same id
        /// </summary>
        /// <param name="id"> group id</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerGroup>> GetCustomerGroup(long id)
        {
            var result = await _groupService.GetCustomerGroup(id);

            return result == null ? NotFound() : result;
        }

        // PUT: api/CustomerGroups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// creates customer group with specified id
        /// returns 404 not found if id doesnt exist
        /// </summary>
        /// <param name="id">id of group</param>
        /// <param name="customerGroup">customerGroup to add</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerGroup(int id, CustomerGroup customerGroup)
        {
            if (id != customerGroup.Id)
            {
                return BadRequest();
            }

            var result = await _groupService.PutCustomerGroup(id, customerGroup);

            return result == null ? NotFound() : NoContent();
        }

        // POST: api/CustomerGroups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Create new customer groip
        /// </summary>
        /// <param name="customerGroup">CustomerGroup to add</param>
        [HttpPost]
        public async Task<ActionResult<CustomerGroup>> PostCustomerGroup(CustomerGroup customerGroup)
        {
            var result = await _groupService.PostCustomerGroup(customerGroup);

            return CreatedAtAction("GetCustomerGroup", new { id = result.Id }, result);
        }

        // DELETE: api/CustomerGroups/5
        /// <summary>
        /// Delete specified group
        /// </summary>
        /// <param name="id">id of group to be deleted</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerGroup(int id)
        {
            var result = await _groupService.DeleteCustomerGroup(id);

            return result == true ? NotFound() : NoContent();
        }
    }
}
