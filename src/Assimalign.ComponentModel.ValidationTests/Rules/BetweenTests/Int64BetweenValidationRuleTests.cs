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

    /*
     NOTES: 
        - If an immutable type was purposely made nullable then validation should only apply when a value is 
          supplied. This means if the value is null then Validation should succeed.
     
     */

    public class Int64BetweenValidationRuleTests
    {

        public partial class Person
        {
            public long Age { get; set; }
            public long? AgeNullable { get; set; }
            public IEnumerable<long> Ages { get; set; }
            public IEnumerable<long>? AgesNullable { get; set; }
            public IEnumerable<long?>? AgesNullable1 { get; set; }
        }


        [Fact]
        public void non_nullable_long_inbounds_success_test()
        {
            var person = new Person() { Age = 15 };
            var context = new ValidationContext<Person>(person);
            var rule = new BetweenValidationRule<Person, long, long>(x => x.Age, 10, 20);

            rule.Evaluate(context);
            Assert.Empty(context.Errors);
        }

        [Fact]
        public void non_nullable_long_out_of_bounds_lessthan_failure_test()
        {
            var person = new Person() { Age = 9 };
            var context = new ValidationContext<Person>(person);
            var rule = new BetweenValidationRule<Person, long, long>(x => x.Age, 10, 20);

            rule.Evaluate(context);
            Assert.Single(context.Errors);
        }

        [Fact]
        public void non_nullable_long_out_of_bounds_greaterthan_failure_test()
        {
            var person = new Person() { Age = 21 };
            var context = new ValidationContext<Person>(person);
            var rule = new BetweenValidationRule<Person, long, long>(x => x.Age, 10, 20);

            rule.Evaluate(context);
            Assert.Single(context.Errors);
        }


        /// <summary>
        /// With value types we only want to compare nullable
        /// </summary>
        [Fact]
        public void nullable_long_null_success_test()
        {
            var person = new Person() { AgeNullable = null };
            var context = new ValidationContext<Person>(person);
            var rule = new BetweenValidationRule<Person, long?, long>(x => x.AgeNullable, 10, 20);

            rule.Evaluate(context);
            Assert.Empty(context.Errors);
        }

        [Fact]
        public void nullable_long_inbound_success_test()
        {
            var person = new Person() { AgeNullable = 15 };
            var context = new ValidationContext<Person>(person);
            var rule = new BetweenValidationRule<Person, long?, long>(x => x.AgeNullable, 10, 20);

            rule.Evaluate(context);
            Assert.Empty(context.Errors);
        }


        [Fact]
        public void nullable_long_out_of_bounds_lessthan_failure_test()
        {
            var person = new Person() { Age = 9 };
            var context = new ValidationContext<Person>(person);
            var rule = new BetweenValidationRule<Person, long, long>(x => x.Age, 10, 20);

            rule.Evaluate(context);
            Assert.Single(context.Errors);
        }

        [Fact]
        public void nullable_long_out_of_bounds_greaterthan_failure_test()
        {
            var person = new Person() { Age = 21 };
            var context = new ValidationContext<Person>(person);
            var rule = new BetweenValidationRule<Person, long, long>(x => x.Age, 10, 20);

            rule.Evaluate(context);
            Assert.Single(context.Errors);
        }
    }
}
