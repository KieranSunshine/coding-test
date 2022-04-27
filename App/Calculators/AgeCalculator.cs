using System;
using App.Providers;

namespace App.Calculators
{
    public interface IAgeCalculator
    {
        int CalculateAge(DateTime dateOfBirth);
    }

    // TODO: Needs unit tests.
    public class AgeCalculator : IAgeCalculator
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public AgeCalculator(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }


        public int CalculateAge(DateTime dateOfBirth)
        {
            var now = _dateTimeProvider.Now();
            
            var age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
                age--;

            return age;
        }

    }
}