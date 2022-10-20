using CustomerContactBook.Database;
using CustomerContactBook.Database.Tables;
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

        public async Task<List<CustomerGroupModel>> GetGroups()
        {
            var result =  await _context.Groups.ToListAsync();
            return result.Select(toGroupModel).ToList();
        }

        public async Task<CustomerGroupModel> GetCustomerGroup(long id)
        {
            var customerGroup = await _context.Groups.FindAsync(id);
            if (customerGroup == null)
            {
                return null;
            }
            return toGroupModel(customerGroup);
        }

        public async Task<bool?> PutCustomerGroup(long id, CustomerGroupModel model)
        {
            var customerGroup = toGroup(model);
            if (customerGroup == null)
            {
                return null;
            }

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

        public async Task<CustomerGroupModel> PostCustomerGroup(CustomerGroupModel model)
        {
            var customerGroup = new CustomerGroup
            {
                Id = model.Id,
                Name = model.Name,
            };

            _context.Groups.Add(customerGroup);
            await _context.SaveChangesAsync();

            return model;
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

        private static CustomerGroupModel toGroupModel(CustomerGroup group)
        {
            return new CustomerGroupModel
            {
                Id = group.Id,
                Name = group.Name,
            };
        }

        private static CustomerGroup toGroup(CustomerGroupModel group)
        {
            return new CustomerGroup
            {
                Id = group.Id,
                Name = group.Name,
            };
        }
    }
}
