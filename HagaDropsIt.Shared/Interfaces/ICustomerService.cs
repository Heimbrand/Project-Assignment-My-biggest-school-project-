namespace HagaDropsIt.Shared.Interfaces;

public interface ICustomerService<T> where T : class
{
    Task<IEnumerable<T>> GetAllCustomers();
    Task<T?> GetCustomerByEmail(string email);
    Task<T?> GetCustomerById(int id);
    Task AddCustomer(T newCustomer);
    Task DeleteCustomer(int id);
    Task<bool> UpdateCustomer(T updatedCustomer);
    Task<T?> GetCustomerByGuid(Guid guid);
    
}