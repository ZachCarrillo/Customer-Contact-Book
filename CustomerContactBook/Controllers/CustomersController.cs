﻿using System;
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
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
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
        public async Task<ActionResult<Customer>> GetCustomer(long id)
        {
            var result = await _customerService.GetCustomer(id);
            return result;
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
        public async Task<IActionResult> PutCustomer(long id, Customer customer)
        {
            var result = await _customerService.PutCustomer(id, customer);
            return result;
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// create customer
        /// </summary>
        /// <param name="customer">customer to create</param>
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            var result = await _customerService.PostCustomer(customer);
            return result;
        }

        // DELETE: api/Customers/5
        /// <summary>
        /// delete customer with same id
        /// </summary>
        /// <param name="id">id of customer to delete</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(long id)
        {
            var result = await _customerService.DeleteCustomer(id);
            return result;
        }
    }
}