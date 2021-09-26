using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Assimalign.ComponentModel.ValidationTests.Rules.BetweenOrEqualTo
{
    using Assimalign.ComponentModel.Validation;
    using Assimalign.ComponentModel.Validation.Rules;

    public class Int32BetweenOrEqualToValidationRuleTests
    {
        public partial class Person
        {
            public int Age { get; set; }
            public int? AgeNullable { get; set; }
            public IEnumerable<int> Ages { get; set; }
            public IEnumerable<int>? AgesNullable { get; set; }
        }


        [Fact]
        public void non_nullable_integer_inbounds_success_test()
        {
            var person = new Person() { Age = 15 };
            var context = new ValidationContext<Person>(person);
            var rule = new BetweenValidationRule<Person, int, int>(x => x.Age, 10, 20);

            rule.Evaluate(context);
            Assert.Empty(context.Errors);
        }

        [Fact]
        public void non_nullable_integer_out_of_bounds_lessthan_failure_test()
        {
            var person = new Person() { Age = 9 };
            var context = new ValidationContext<Person>(person);
            var rule = new BetweenValidationRule<Person, int, int>(x => x.Age, 10, 20);

            rule.Evaluate(context);
            Assert.Single(context.Errors);
        }

        [Fact]
        public void non_nullable_integer_out_of_bounds_greaterthan_failure_test()
        {
            var person = new Person() { Age = 21 };
            var context = new ValidationContext<Person>(person);
            var rule = new BetweenValidationRule<Person, int, int>(x => x.Age, 10, 20);

            rule.Evaluate(context);
            Assert.Single(context.Errors);
        }
    }
}
