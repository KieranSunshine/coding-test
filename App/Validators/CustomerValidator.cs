using System;

namespace App.Validators
{
    public interface ICustomerValidator
    {
        bool ValidateNames(params string[] names);

        bool ValidateEmail(string email);
    }

    public class CustomerValidator
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