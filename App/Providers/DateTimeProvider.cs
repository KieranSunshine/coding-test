using System;

namespace App.Providers
{
    public interface IDateTimeProvider
    {
        DateTime Now();
    }
    
    public class DateTimeProvider
    {
        public DateTime Now() => DateTime.Now;
    }
}