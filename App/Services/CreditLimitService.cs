using App.Model;
using App.Model.Constants;

namespace App.Services
{
    public interface ICreditLimitService
    {
        bool HasCreditLimit(string companyName);
        int GetCreditLimit(Customer customer);
    }
    
    // TODO: Needs Unit Tests.
    // TODO: Figure out a way to remove dependency on concrete CustomerCreditServiceClient.
    public class CreditLimitService : ICreditLimitService
    {
        public bool HasCreditLimit(string companyName)
        {
            switch (companyName)
            {
                case CompanyNames.VeryImportantClient:
                    return false;
                
                case CompanyNames.ImportantClient:
                    return true;
                
                default:
                    return true;
            }
        }

        public int GetCreditLimit(Customer customer)
        {
            // TODO: Check company is populated.
            int limit;
            switch (customer.Company.Name)
            {
                case CompanyNames.VeryImportantClient:
                    limit = customer.CreditLimit;    // Do nothing.
                    break;

                case CompanyNames.ImportantClient:
                {
                    using (var customerCreditService = new CustomerCreditServiceClient())
                    {
                        var creditLimit = customerCreditService.GetCreditLimit(customer.Firstname, customer.Surname, customer.DateOfBirth);
                        limit = creditLimit * 2;    // Double.
                    }

                    break;
                }

                default:
                {
                    using (var customerCreditService = new CustomerCreditServiceClient())
                    {
                        limit = customerCreditService.GetCreditLimit(customer.Firstname, customer.Surname, customer.DateOfBirth);
                    }

                    break;
                }
            }

            return limit;
        }
    }
}