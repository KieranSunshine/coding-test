using System;
using App.Calculators;
using App.Validators;
using Moq;
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
            var validator = CreateValidatorInstance();

            Assert.Throws<ArgumentNullException>(() => validator.ValidateNames(null));
        }

        [Test]
        [Parallelizable]
        public void ValidateNamesThrowsOutOfRangeExceptionOnEmptyArray()
        {
            var validator = CreateValidatorInstance();

            Assert.Throws<ArgumentOutOfRangeException>(() => validator.ValidateNames());
        }

        [Test]
        [Parallelizable]
        public void ValidateNamesReturnsTrueForValidNames()
        {
            var validator = CreateValidatorInstance();

            var result = validator.ValidateNames("John", "Doe");
            
            Assert.IsTrue(result);
        }

        [Test]
        [Parallelizable]
        public void ValidateNamesReturnsFalseForInvalidNames()
        {
            var validator = CreateValidatorInstance();

            var result = validator.ValidateNames("John", null);
            
            Assert.IsFalse(result);
        }

        #endregion

        #region ValidateEmail

        [Test]
        [Parallelizable]
        public void ValidateEmailReturnsFalseForNull()
        {
            var validator = CreateValidatorInstance();

            var result = validator.ValidateEmail(null);
            
            Assert.IsFalse(result);
        }

        [Test]
        [Parallelizable]
        public void ValidateEmailReturnsFalseForEmptyString()
        {
            var validator = CreateValidatorInstance();

            var result = validator.ValidateEmail(string.Empty);
            
            Assert.IsFalse(result);
        }
        
        [Test]
        [Parallelizable]
        public void ValidateEmailReturnsFalseForInvalidEmail()
        {
            var validator = CreateValidatorInstance();

            var result = validator.ValidateEmail("not_an_email");
            
            Assert.IsFalse(result);
        }
        
        [Test]
        [Parallelizable]
        public void ValidateEmailReturnsTrueForValidEmail()
        {
            var validator = CreateValidatorInstance();

            var result = validator.ValidateEmail("john.doe@email.com");
            
            Assert.IsTrue(result);
        }
        
        #endregion

        #region MyRegion

        [Test]
        [Parallelizable]
        public void ValidateAgeCallsCalculateAge()
        {
            var stubbedAge = new DateTime(1990, 1, 1);
            var mockAgeCalculator = new Mock<IAgeCalculator>();
            
            mockAgeCalculator
                .Setup(m => m.CalculateAge(It.IsAny<DateTime>()))
                .Returns(30)
                .Verifiable();

            var validator = CreateValidatorInstance(mockAgeCalculator);
            validator.ValidateAge(stubbedAge);
            
            mockAgeCalculator.Verify();
        }

        [Test]
        [Parallelizable]
        public void ValidateAgeReturnsTrueForValidAge()
        {
            var stubbedAge = new DateTime(1990, 1, 1);
            var mockAgeCalculator = new Mock<IAgeCalculator>();
            
            mockAgeCalculator
                .Setup(m => m.CalculateAge(It.IsAny<DateTime>()))
                .Returns(30);

            var validator = CreateValidatorInstance(mockAgeCalculator);
            var result = validator.ValidateAge(stubbedAge);
            
            Assert.IsTrue(result);
        }
        
        // TODO: Test for false cases.

        #endregion

        private static CustomerValidator CreateValidatorInstance()
        {
            var mockAgeCalculator = new Mock<IAgeCalculator>();
            
            return new CustomerValidator(mockAgeCalculator.Object);
        }

        private CustomerValidator CreateValidatorInstance(IMock<IAgeCalculator> mockAgeCalculator)
        {
            return new CustomerValidator(mockAgeCalculator.Object);
        }

    }
}