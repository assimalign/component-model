using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Assimalign.ComponentModel.ValidationTests.RulesTests.BetweenTests
{
    using Assimalign.ComponentModel.Validation;
    using Assimalign.ComponentModel.Validation.Rules;

    public class Int16BetweenValidationRuleTests
    {
        public partial class Person
        {
            public int Age { get; set; }
            public IEnumerable<int> Ages { get; set; }
            public int? AgeNullable { get; set; }
            public IEnumerable<int>? AgesNullable { get; set; }
            public IEnumerable<int?>? AgesNullable1 { get; set; }
        }


        [Fact]
        public void DateTimeValidationMemberLowerBoundFailureTest()
        {
            var person = new Person() { AgeNullable = null };
            var context = new ValidationContext<Person>(person);
            var rule = new BetweenValidationRule<Person, int?, int, int>(x => x.AgeNullable, 10,24);

            rule.Evaluate(context);
            Assert.Single(context.Errors);
        }
    }
}
