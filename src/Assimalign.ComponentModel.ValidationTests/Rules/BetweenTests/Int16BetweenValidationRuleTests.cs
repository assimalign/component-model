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

    public class Int16BetweenValidationRuleTests
    {
        public partial class Person
        {
            public short Age { get; set; }
            public short? AgeNullable { get; set; }
            public IEnumerable<short> Ages { get; set; }
            public IEnumerable<short>? AgesNullable { get; set; }
        }


        [Fact]
        public void non_nullable_short_inbounds_success_test()
        {
            var person = new Person() { Age = 15 };
            var context = new ValidationContext<Person>(person);
            var rule = new BetweenValidationRule<Person, short, short>(x => x.Age, 10, 20);

            rule.Evaluate(context);
            Assert.Empty(context.Errors);
        }

        [Fact]
        public void non_nullable_short_out_of_bounds_lessthan_failure_test()
        {
            var person = new Person() { Age = 9 };
            var context = new ValidationContext<Person>(person);
            var rule = new BetweenValidationRule<Person, short, short>(x => x.Age, 10, 20);

            rule.Evaluate(context);
            Assert.Single(context.Errors);
        }

        [Fact]
        public void non_nullable_short_out_of_bounds_greaterthan_failure_test()
        {
            var person = new Person() { Age = 21 };
            var context = new ValidationContext<Person>(person);
            var rule = new BetweenValidationRule<Person, short, short>(x => x.Age, 10, 20);

            rule.Evaluate(context);
            Assert.Single(context.Errors);
        }
    }
}
