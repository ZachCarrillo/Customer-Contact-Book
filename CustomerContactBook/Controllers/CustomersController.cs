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
    public class CustomersController : ControllerBase
    {
        private readonly CustomerContext _context;

        public CustomersController(CustomerContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        /// <summary>
        /// return all customers
        /// </summary>
        [HttpGet]
<<<<<<< Updated upstream
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
=======
        [ProducesResponseType(typeof(List<CustomerModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CustomerModel>>> GetCustomers()
>>>>>>> Stashed changes
        {
            return await _context.Customers.ToListAsync();
        }

        // GET: api/Customers/5
        /// <summary>
        /// return customer with same id
        /// </summary>
        /// <param name="id">customer id</param>
        [HttpGet("{id}")]
<<<<<<< Updated upstream
        public async Task<ActionResult<Customer>> GetCustomer(int id)
=======
        [ProducesResponseType(typeof(CustomerModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerModel>> GetCustomer(long id)
>>>>>>> Stashed changes
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// updates customer with specified id if exists
        /// returns 404 not found if id doesnt exist
        /// </summary>
        /// <param name="id">id of customer</param>
        /// <param name="customer">Customer to create</param>
        [HttpPut("{id}")]
<<<<<<< Updated upstream
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
=======
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutCustomer(long id, CustomerModel customer)
>>>>>>> Stashed changes
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }
<<<<<<< Updated upstream

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!CustomerExists(id))
            {
                return NotFound();
            }

            return NoContent();
=======
            var result = await _customerService.UpdateCustomer(id, customer);
            return result == false ? NotFound() : NoContent();
>>>>>>> Stashed changes
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// create customer
        /// </summary>
        /// <param name="customer">customer to create</param>
        [HttpPost]
<<<<<<< Updated upstream
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
=======
        [ProducesResponseType(typeof(CustomerModel),StatusCodes.Status201Created)]
        public async Task<ActionResult<CustomerModel>> PostCustomer(CustomerModel customer)
        {
            var result = await _customerService.CreateCustomer(customer);
            return CreatedAtAction("GetCustomer", new { id = result.Id }, result);
>>>>>>> Stashed changes
        }

        // DELETE: api/Customers/5
        /// <summary>
        /// delete customer with same id
        /// </summary>
        /// <param name="id">id of customer to delete</param>
        [HttpDelete("{id}")]
<<<<<<< Updated upstream
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
=======
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCustomer(long id)
>>>>>>> Stashed changes
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
