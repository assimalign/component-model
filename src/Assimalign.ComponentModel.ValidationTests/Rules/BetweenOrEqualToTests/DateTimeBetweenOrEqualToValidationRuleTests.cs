using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Assimalign.ComponentModel.ValidationTests.Rules.BetweenOrEqualTo
{
    using Assimalign.ComponentModel.Validation;
    using Assimalign.ComponentModel.Validation.Internal.Rules;

    public class DateTimeBetweenOrEqualToValidationRuleTests
    {
        public partial class TestClass
        {
            public DateTime Birthdate { get; set; }
            public DateTime? BirthdateNullable { get; set; }
            public IEnumerable<DateTime> Birthdates { get; set; }
            public IEnumerable<DateTime?> BirthdatesNullable { get; set; }
            public IEnumerable<DateTime?>? BirthdatesNullable1 { get; set; }
            public IEnumerable<DateTime>? BirthdatesNullable2 { get; set; }
        }

        [Fact]
        public void EnumerableDateTimeValidationMemberSuccessTest()
        {
            var person = new TestClass() 
            { 
                Birthdates = new DateTime[] 
                {
                    new DateTime(1997, 01, 01)
                }
            };
            var context = new ValidationContext<TestClass>(person);
            var rule = new BetweenOrEqualToValidationRule<TestClass, IEnumerable<DateTime>, DateTime>(x => x.Birthdates, new DateTime(1996, 01, 01), new DateTime(2010, 01, 01));

            rule.Evaluate(context);
            Assert.Empty(context.Errors);
        }


        [Fact]
        public void NullableEnumerableDateTimeValidationMemberSuccessTest()
        {
            var person = new TestClass()
            {
                BirthdatesNullable = new DateTime?[]
                {
                    new DateTime(1997, 01, 01)
                }
            };
            var context = new ValidationContext<TestClass>(person);
            var rule = new BetweenOrEqualToValidationRule<TestClass, IEnumerable<DateTime?>, DateTime>(x => x.BirthdatesNullable, new DateTime(1996, 01, 01), new DateTime(2010, 01, 01));

            rule.Evaluate(context);
            Assert.Empty(context.Errors);
        }


        [Fact]
        public void NullableEnumerableDateTimeValidationMemberFailureTest()
        {
            var person = new TestClass()
            {
                BirthdatesNullable = new DateTime?[]
                {
                    new DateTime(1997, 01, 01),
                    null
                }
            };
            var context = new ValidationContext<TestClass>(person);
            var rule = new BetweenOrEqualToValidationRule<TestClass, IEnumerable<DateTime?>, DateTime>(x => x.BirthdatesNullable, new DateTime(1996, 01, 01), new DateTime(2010, 01, 01));

            rule.Evaluate(context);
            Assert.Empty(context.Errors);
        }

        [Fact]
        public void DateTimeValidationMemberSuccessTest()
        {
            var person = new TestClass() { Birthdate = new DateTime(1997,01,01) };
            var context = new ValidationContext<TestClass>(person);
            var rule = new BetweenOrEqualToValidationRule<TestClass, DateTime, DateTime>(x => x.Birthdate, new DateTime(1996,01,01), new DateTime(2010,01,01));

            rule.Evaluate(context);
            Assert.Empty(context.Errors);
        }


        [Fact]
        public void DateTimeValidationMemberUpperBoundFailureTest()
        {
            var person = new TestClass() { Birthdate = new DateTime(2011, 01, 01) };
            var context = new ValidationContext<TestClass>(person);
            var rule = new BetweenOrEqualToValidationRule<TestClass, DateTime, DateTime>(x => x.Birthdate, new DateTime(1996, 01, 01), new DateTime(2010, 01, 01))
            {
                Error = new ValidationError()
                {
                    Code = "400",
                    Message = "Test Error Message",
                    Source = "Birthdate"
                }
            };

            rule.Evaluate(context);
            Assert.Single(context.Errors);
        }


        [Fact]
        public void DateTimeValidationMemberLowerBoundFailureTest()
        {
            var person = new TestClass() { Birthdate = new DateTime(1995, 01, 01) };
            var context = new ValidationContext<TestClass>(person);
            var rule = new BetweenOrEqualToValidationRule<TestClass, DateTime, DateTime>(x => x.Birthdate, new DateTime(1996, 01, 01), new DateTime(2010, 01, 01))
            {
                Error = new ValidationError()
                {
                    Code = "400",
                    Message = "Test Error Message",
                    Source = "Birthdate"
                }
            };

            rule.Evaluate(context);
            Assert.Single(context.Errors);
        }


        [Fact]
        public void DateTimeValidationMemberLowerBoundNullFailureTest()
        {
            var person = new TestClass() { BirthdateNullable = null };
            var context = new ValidationContext<TestClass>(person);
            var rule = new BetweenOrEqualToValidationRule<TestClass, DateTime, DateTime>(x => x.Birthdate, new DateTime(1996, 01, 01), new DateTime(2010, 01, 01))
            {
                Error = new ValidationError()
                {
                    Code = "400",
                    Message = "Test Error Message",
                    Source = "Birthdate"
                }
            };

            rule.Evaluate(context);
            Assert.Single(context.Errors);
        }
    }
}
