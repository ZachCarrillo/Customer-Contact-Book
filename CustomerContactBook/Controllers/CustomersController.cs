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
    public class CustomersController : ControllerBase
    {
        private readonly CustomersService _customerService;

        public CustomersController(CustomersService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/Customers
        /// <summary>
        /// return all customers
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<CustomerModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CustomerModel>>> GetCustomers()
        {
            var result = await _customerService.GetCustomers();
            return result;
        }

        // GET: api/Customers/5
        /// <summary>
        /// return customer with same id
        /// </summary>
        /// <param name="id">customer id</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerModel>> GetCustomer(long id)
        {
            var result = await _customerService.GetCustomer(id);
            return result == null ? NotFound() : result;
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutCustomer(long id, CustomerModel customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }
            var result = await _customerService.UpdateCustomer(id, customer);
            return result == false ? NotFound() : NoContent();
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// create customer
        /// </summary>
        /// <param name="customer">customer to create</param>
        [HttpPost]
        [ProducesResponseType(typeof(CustomerModel), StatusCodes.Status201Created)]

        public async Task<ActionResult<CustomerModel>> PostCustomer(CustomerModel customer)
        {
            var result = await _customerService.CreateCustomer(customer);
            return CreatedAtAction("GetCustomer", new { id = result.Id }, result);
        }

        // DELETE: api/Customers/5
        /// <summary>
        /// delete customer with same id
        /// </summary>
        /// <param name="id">id of customer to delete</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCustomer(long id)
        {
            var result = await _customerService.DeleteCustomer(id);
            return result == false ? NotFound() : NoContent();
        }
    }
}
