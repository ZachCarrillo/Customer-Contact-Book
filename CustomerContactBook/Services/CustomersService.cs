using CustomerContactBook.Database;
using CustomerContactBook.Database.Tables;
using CustomerContactBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomerContactBook.Services
{
    public class CustomersService
    {
        private readonly ContactBookContext _context;

        public CustomersService(ContactBookContext context)
        {
            _context = context;
        }

        public async Task<List<CustomerModel>> GetCustomers()
        {
            var customers =  await _context.Customers.ToListAsync();
            return customers.Select(ToCustomerModel).ToList();
        }

        public async Task<CustomerModel> GetCustomer(long id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return null;
            }

            return ToCustomerModel(customer);
        }

        public async Task<bool> PutCustomer(long id, CustomerModel model)
        {
            var customer = ToCustomer(model);
            if (customer == null)
            {
                return false;
            }
            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!CustomerExists(id))
            {
                return false;
            }

            return true;
        }

        public async Task<CustomerModel> PostCustomer(CustomerModel model)
        {
            var customer = new Customer
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNum = model.PhoneNum,
                Email = model.Email,
                Address = model.Address,
                BirthDay = model.BirthDay,
            };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return ToCustomerModel(customer);
        }

        public async Task<bool> DeleteCustomer(long id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return false;
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool CustomerExists(long id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }

        private static CustomerModel ToCustomerModel(Customer customer)
        {
            return new CustomerModel
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PhoneNum = customer.PhoneNum,
                Email = customer.Email,
                Address = customer.Address,
                BirthDay = customer.BirthDay,
            };
        }

        private static Customer ToCustomer(CustomerModel customer)
        {
            return new Customer
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PhoneNum = customer.PhoneNum,
                Email = customer.Email,
                Address = customer.Address,
                BirthDay = customer.BirthDay,
            };
        }
    }
}
