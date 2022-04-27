using System;
using App.Calculators;

namespace App.Validators
{
    public interface ICustomerValidator
    {
        /// <summary>
        /// Tests each name in the array to ensure that it meets the acceptable criteria.
        /// </summary>
        /// <param name="names">The names to test.</param>
        /// <returns>Returns true when all names are valid, otherwise false.</returns>
        bool ValidateNames(params string[] names);

        /// <summary>
        /// Tests the provided string to determine whether it is a valid email address.
        /// </summary>
        /// <param name="email">The string to test.</param>
        /// <returns>Returns true if the provided string is a valid email address, otherwise false.</returns>
        bool ValidateEmail(string email);

        /// <summary>
        /// Validates that the provided date of birth is valid.
        /// </summary>
        /// <param name="dateOfBirth">The date of birth to test.</param>
        /// <returns>Returns true if the date is over the minimum age. Otherwise false.</returns>
        bool ValidateAge(DateTime dateOfBirth);
    }

    public class CustomerValidator : ICustomerValidator
    {
        private const int MinimumAge = 21;
        private readonly IAgeCalculator _ageCalculator;

        public CustomerValidator(IAgeCalculator ageCalculator)
        {
            _ageCalculator = ageCalculator;
        }

        public bool ValidateNames(params string[] names)
        {
            if (names is null)
                throw new ArgumentNullException(nameof(names));
            
            if (names.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(names));
            
            foreach (var name in names)
            {
                if (!NameIsValid(name))
                    return false;
            }

            return true;
        }

        public bool ValidateEmail(string email) => !string.IsNullOrWhiteSpace(email) &&
                                                   email.Contains("@") &&
                                                   email.Contains(".");
        
        public bool ValidateAge(DateTime dateOfBirth)
        {
            var age = _ageCalculator.CalculateAge(dateOfBirth);
            return age > MinimumAge;
        }

        private bool NameIsValid(string name) => !string.IsNullOrWhiteSpace(name);
    }
}