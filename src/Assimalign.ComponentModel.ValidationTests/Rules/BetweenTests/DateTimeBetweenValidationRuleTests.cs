using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Assimalign.ComponentModel.ValidationTests.Rules.Between
{
    using Assimalign.ComponentModel.Validation;
    using Assimalign.ComponentModel.Validation.Internal.Rules;

    public class DateTimeBetweenValidationRuleTests
    {
        

        public partial class Person
        {
            public DateTime Birthdate { get; set; }
            public IEnumerable<DateTime> Birthdates { get; set; }
            public DateTime? BirthdateNullable { get; set; }
            public IEnumerable<DateTime?>? BirthdatesNullable { get; set; }
            public IEnumerable<DateTime?>? BirthdatesNullable1 { get; set; }
        }

        [Fact]
        public void DateTimeValidationMemberSuccessTest()
        {
            var person = new Person() { Birthdate = new DateTime(1997,01,01) };
            var context = new ValidationContext<Person>(person);
            var rule = new BetweenValidationRule<Person, DateTime, DateTime>(x => x.Birthdate, new DateTime(1996,01,01), new DateTime(2010,01,01));

            rule.Evaluate(context);
            Assert.Empty(context.Errors);
        }


        [Fact]
        public void DateTimeValidationMemberUpperBoundFailureTest()
        {
            var person = new Person() { Birthdate = new DateTime(2011, 01, 01) };
            var context = new ValidationContext<Person>(person);
            var rule = new BetweenValidationRule<Person, DateTime, DateTime>(x => x.Birthdate, new DateTime(1996, 01, 01), new DateTime(2010, 01, 01));

            rule.Evaluate(context);
            Assert.Single(context.Errors);
        }


        [Fact]
        public void DateTimeValidationMemberLowerBoundFailureTest()
        {
            var person = new Person() { Birthdate = new DateTime(1995, 01, 01) };
            var context = new ValidationContext<Person>(person);
            var rule = new BetweenValidationRule<Person, DateTime, DateTime>(x => x.Birthdate, new DateTime(1996, 01, 01), new DateTime(2010, 01, 01));

            rule.Evaluate(context);
            Assert.Single(context.Errors);
        }


        [Fact]
        public void DateTimeValidationMemberLowerBoundNullFailureTest()
        {
            var person = new Person() { BirthdateNullable = null };
            var context = new ValidationContext<Person>(person);
            var rule = new BetweenValidationRule<Person, DateTime, DateTime>(x => x.Birthdate, new DateTime(1996, 01, 01), new DateTime(2010, 01, 01));

            rule.Evaluate(context);
            Assert.Single(context.Errors);
        }
    }
}
