using App.Data;
using App.Model;

namespace App.Providers
{
    public interface ICustomerDataAccessProvider
    {
        void AddCustomer(Customer customer);
    }
    
    public class CustomerDataAccessProvider : ICustomerDataAccessProvider
    {
        public void AddCustomer(Customer customer)
        {
            CustomerDataAccess.AddCustomer(customer);
        }
    }
}