using System;
using App.Data;
using App.Data.Repositories;
using App.Model;
using App.Providers;
using App.Validators;

namespace App.Services
{
    public class CustomerService
    {
        private readonly ICustomerValidator _customerValidator;
        private IDateTimeProvider _dateTimeProvider;

        public CustomerService(
            ICustomerValidator customerValidator,
            IDateTimeProvider dateTimeProvider)
        {
            _customerValidator = customerValidator;
            _dateTimeProvider = dateTimeProvider;
        }

        public bool AddCustomer(string firstName, string surname, string email, DateTime dateOfBirth, int companyId)
        {
            if (!_customerValidator.ValidateNames(firstName, surname) || !_customerValidator.ValidateEmail(email))
                return false;

            var now = _dateTimeProvider.Now();
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            if (age < 21)
            {
                return false;
            }

            var companyRepository = new CompanyRepository();
            var company = companyRepository.GetById(companyId);

            var customer = new Customer
                               {
                                   Company = company,
                                   DateOfBirth = dateOfBirth,
                                   EmailAddress = email,
                                   Firstname = firstName,
                                   Surname = surname
                               };

            if (company.Name == "VeryImportantClient")
            {
                // Skip credit check
                customer.HasCreditLimit = false;
            }
            else if (company.Name == "ImportantClient")
            {
                // Do credit check and double credit limit
                customer.HasCreditLimit = true;
                using (var customerCreditService = new CustomerCreditServiceClient())
                {
                    var creditLimit = customerCreditService.GetCreditLimit(customer.Firstname, customer.Surname, customer.DateOfBirth);
                    creditLimit = creditLimit*2;
                    customer.CreditLimit = creditLimit;
                }
            }
            else
            {
                // Do credit check
                customer.HasCreditLimit = true;
                using (var customerCreditService = new CustomerCreditServiceClient())
                {
                    var creditLimit = customerCreditService.GetCreditLimit(customer.Firstname, customer.Surname, customer.DateOfBirth);
                    customer.CreditLimit = creditLimit;
                }
            }

            if (customer.HasCreditLimit && customer.CreditLimit < 500)
            {
                return false;
            }

            CustomerDataAccess.AddCustomer(customer);

            return true;
        }
    }
}
