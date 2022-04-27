using System;

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
    }

    public class CustomerValidator : ICustomerValidator
    {
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
        private bool NameIsValid(string name) => !string.IsNullOrWhiteSpace(name);
    }
}