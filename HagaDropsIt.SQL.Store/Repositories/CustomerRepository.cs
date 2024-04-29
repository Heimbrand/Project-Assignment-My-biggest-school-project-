using HagaDropsIt.Shared.Entities;
using HagaDropsIt.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HagaDropsIt.SQL.Store.Repositories;

public class CustomerRepository(StoreDbContext context) : ICustomerService<Customer>
{
    public async Task<IEnumerable<Customer>> GetAllCustomers()
    {
        return await context.Customers.ToListAsync();
    }

    public async Task<Customer?> GetCustomerByEmail(string email)
    {
        return await context.Customers.FirstOrDefaultAsync(c => c.Email == email);
    }

    public async Task<Customer?> GetCustomerById(int id)
    {
        return await context.Customers.FindAsync(id);
    }

    public async Task AddCustomer(Customer newCustomer)
    {
        await context.Customers.AddAsync(newCustomer);
        await context.SaveChangesAsync();
    }

    public async Task DeleteCustomer(int id)
    {
        var dCustomer = await context.Customers.FindAsync(id);
        if (dCustomer == null)
        {
            return;
        }

        context.Customers.Remove(dCustomer);
        await context.SaveChangesAsync();
    }

    public async Task<bool> UpdateCustomer(Customer updatedCustomer)
    {
        var customerToUpdate = await context.Customers.FindAsync(updatedCustomer.Id);
        if (customerToUpdate == null)
        {
            await context.AddAsync(updatedCustomer);
            await context.SaveChangesAsync();
            return true;
        }

        customerToUpdate.CustomerGuid = updatedCustomer.CustomerGuid;
        customerToUpdate.Email = updatedCustomer.Email;
        customerToUpdate.Cart = updatedCustomer.Cart;

        await context.SaveChangesAsync();
        return true;
    }

    public async Task<Customer?> GetCustomerByGuid(Guid guid)
    {
        return await context.Customers.FirstOrDefaultAsync(c => c.CustomerGuid == guid);
    }



}