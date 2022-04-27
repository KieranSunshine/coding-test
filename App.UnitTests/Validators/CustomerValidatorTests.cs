using System;
using App.Validators;
using NUnit.Framework;

namespace App.UnitTests.Validators
{
    [TestFixture]
    public class CustomerValidatorTests
    {
        #region ValidateNames

        [Test]
        [Parallelizable]
        public void ValidateNamesThrowsArgumentNullExceptionOnNullArray()
        {
            var validator = new CustomerValidator();

            Assert.Throws<ArgumentNullException>(() => validator.ValidateNames(null));
        }

        [Test]
        [Parallelizable]
        public void ValidateNamesThrowsOutOfRangeExceptionOnEmptyArray()
        {
            var validator = new CustomerValidator();

            Assert.Throws<ArgumentOutOfRangeException>(() => validator.ValidateNames());
        }

        [Test]
        [Parallelizable]
        public void ValidateNamesReturnsTrueForValidNames()
        {
            var validator = new CustomerValidator();

            var result = validator.ValidateNames("John", "Doe");
            
            Assert.IsTrue(result);
        }

        [Test]
        [Parallelizable]
        public void ValidateNamesReturnsFalseForInvalidNames()
        {
            var validator = new CustomerValidator();

            var result = validator.ValidateNames("John", null);
            
            Assert.IsFalse(result);
        }

        #endregion

        #region ValidateEmail

        [Test]
        [Parallelizable]
        public void ValidateEmailReturnsFalseForNull()
        {
            var validator = new CustomerValidator();

            var result = validator.ValidateEmail(null);
            
            Assert.IsFalse(result);
        }

        [Test]
        [Parallelizable]
        public void ValidateEmailReturnsFalseForEmptyString()
        {
            var validator = new CustomerValidator();

            var result = validator.ValidateEmail(string.Empty);
            
            Assert.IsFalse(result);
        }
        
        [Test]
        [Parallelizable]
        public void ValidateEmailReturnsFalseForInvalidEmail()
        {
            var validator = new CustomerValidator();

            var result = validator.ValidateEmail("not_an_email");
            
            Assert.IsFalse(result);
        }
        
        [Test]
        [Parallelizable]
        public void ValidateEmailReturnsTrueForValidEmail()
        {
            var validator = new CustomerValidator();

            var result = validator.ValidateEmail("john.doe@email.com");
            
            Assert.IsTrue(result);
        }
        
        #endregion
    }
}