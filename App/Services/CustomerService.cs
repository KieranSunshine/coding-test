using System;
using App.Data.Repositories;
using App.Model;
using App.Providers;
using App.Validators;

namespace App.Services
{
    public class CustomerService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ICustomerValidator _customerValidator;
        private readonly ICreditLimitService _creditLimitService;
        private readonly ICustomerDataAccessProvider _customerDataAccessProvider;

        // TODO: Needs unit tests.
        public CustomerService(
            ICustomerValidator customerValidator,
            ICompanyRepository companyRepository,
            ICreditLimitService creditLimitService,
            ICustomerDataAccessProvider customerDataAccessProvider)
        {
            _customerValidator = customerValidator;
            _companyRepository = companyRepository;
            _creditLimitService = creditLimitService;
            _customerDataAccessProvider = customerDataAccessProvider;
        }

        public bool AddCustomer(string firstName, string surname, string email, DateTime dateOfBirth, int companyId)
        {
            if (!_customerValidator.ValidateNames(firstName, surname) ||
                !_customerValidator.ValidateEmail(email) ||
                !_customerValidator.ValidateAge(dateOfBirth))
                return false;
            
            var company = _companyRepository.GetById(companyId);
            var customer = new Customer
                           {
                               Company = company,
                               DateOfBirth = dateOfBirth,
                               EmailAddress = email,
                               Firstname = firstName,
                               Surname = surname
                           };

            customer.HasCreditLimit = _creditLimitService.HasCreditLimit(company.Name);
            customer.CreditLimit = _creditLimitService.GetCreditLimit(customer);

            if (customer.HasCreditLimit && customer.CreditLimit < 500)
            {
                return false;
            }

            try
            {
                _customerDataAccessProvider.AddCustomer(customer);
            }
            catch (Exception e)
            {
                // Would write error to log.
                
                return false;
            }

            return true;
        }
    }
}
