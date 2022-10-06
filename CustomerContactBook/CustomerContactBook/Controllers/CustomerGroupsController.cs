using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomerContactBook.Models;

namespace CustomerContactBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerGroupsController : ControllerBase
    {
        private readonly CustomerGroupContext _context;

        public CustomerGroupsController(CustomerGroupContext context)
        {
            _context = context;
        }

        // GET: api/CustomerGroups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerGroup>>> GetGroups()
        {
            return await _context.Groups.ToListAsync();
        }

        // GET: api/CustomerGroups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerGroup>> GetCustomerGroup(int id)
        {
            var customerGroup = await _context.Groups.FindAsync(id);

            if (customerGroup == null)
            {
                return NotFound();
            }

            return customerGroup;
        }

        // PUT: api/CustomerGroups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerGroup(int id, CustomerGroup customerGroup)
        {
            if (id != customerGroup.Id)
            {
                return BadRequest();
            }

            _context.Entry(customerGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!CustomerGroupExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/CustomerGroups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerGroup>> PostCustomerGroup(CustomerGroup customerGroup)
        {
            _context.Groups.Add(customerGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomerGroup", new { id = customerGroup.Id }, customerGroup);
        }

        // DELETE: api/CustomerGroups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerGroup(int id)
        {
            var customerGroup = await _context.Groups.FindAsync(id);
            if (customerGroup == null)
            {
                return NotFound();
            }

            _context.Groups.Remove(customerGroup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerGroupExists(int id)
        {
            return _context.Groups.Any(e => e.Id == id);
        }
    }
}
