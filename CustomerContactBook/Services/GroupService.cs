using CustomerContactBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomerContactBook.Services
{
    public class GroupService
    {
        private readonly ContactBookContext _context;

        public GroupService(ContactBookContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<CustomerGroup>>> GetGroups()
        {
            return await _context.Groups.ToListAsync();
        }

        public async Task<ActionResult<CustomerGroup>> GetCustomerGroup(long id)
        {
            var customerGroup = await _context.Groups.FindAsync(id);
            return customerGroup;
        }

        public async Task<bool?> PutCustomerGroup(long id, CustomerGroup customerGroup)
        {

            _context.Entry(customerGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!CustomerGroupExists(id))
            {
                return null;
            }

            return true;
        }

        public async Task<CustomerGroup> PostCustomerGroup(CustomerGroup customerGroup)
        {
            _context.Groups.Add(customerGroup);
            await _context.SaveChangesAsync();

            return customerGroup;
        }

        public async Task<bool> DeleteCustomerGroup(long id)
        {
            var customerGroup = await _context.Groups.FindAsync(id);
            if (customerGroup == null)
            {
                return false;
            }

            _context.Groups.Remove(customerGroup);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool CustomerGroupExists(long id)
        {
            return _context.Groups.Any(e => e.Id == id);
        }
    }
}
