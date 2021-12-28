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
            public DateTime DateTime { get; set; }
            public DateTime? DateTimeNullable { get; set; }
            public IEnumerable<DateTime> DateTimes { get; set; }
            public IEnumerable<DateTime?> DateTimesNullable { get; set; }
            public IEnumerable<DateTime?>? DateTimesNullable1 { get; set; }
            public IEnumerable<DateTime>? DateTimesNullable2 { get; set; }
        }

        [Fact]
        public void EnumerableDateTimeValidationMemberSuccessTest()
        {
            var person = new TestClass() 
            { 
                DateTimes = new DateTime[] 
                {
                    new DateTime(1997, 01, 01)
                }
            };
            var context = new ValidationContext<TestClass>(person);
            var rule = new BetweenOrEqualToValidationRule<TestClass, IEnumerable<DateTime>, DateTime>(x => x.DateTimes, new DateTime(1996, 01, 01), new DateTime(2010, 01, 01));

            rule.Evaluate(context);
            Assert.Empty(context.Errors);
        }


        [Fact]
        public void NullableEnumerableDateTimeValidationMemberSuccessTest()
        {
            var person = new TestClass()
            {
                DateTimesNullable = new DateTime?[]
                {
                    new DateTime(1997, 01, 01)
                }
            };
            var context = new ValidationContext<TestClass>(person);
            var rule = new BetweenOrEqualToValidationRule<TestClass, IEnumerable<DateTime?>, DateTime>(x => x.DateTimesNullable, new DateTime(1996, 01, 01), new DateTime(2010, 01, 01));

            rule.Evaluate(context);
            Assert.Empty(context.Errors);
        }




        #region DateTime Enumerable (Nullable DateTime and Non-Nullable IEnumerable)


        [Fact]
        public void DateTimesNullableWithNonNullableEnumerableWithinSuccessTest()
        {
            var testClass = new TestClass()
            {
                DateTimesNullable = new DateTime?[]
                {
                    new DateTime(1997, 01, 01),
                    new DateTime(2005, 01, 01)
                }
            };
            var context = new ValidationContext<TestClass>(testClass);
            var rule = new BetweenOrEqualToValidationRule<TestClass, IEnumerable<DateTime?>, DateTime>(
                expression: x => x.DateTimesNullable,
                lower: new DateTime(1996, 01, 01),
                upper: new DateTime(2010, 01, 01));

            rule.Evaluate(context);
            Assert.Empty(context.Errors);
        }


        [Fact]
        public void DateTimesNullableWithNonNullableEnumerableWithinFailureTest()
        {
            var testClass = new TestClass()
            {
                DateTimesNullable = new DateTime?[]
                {
                    new DateTime(1997, 01, 01),
                    null
                }
            };
            var context = new ValidationContext<TestClass>(testClass);
            var rule = new BetweenOrEqualToValidationRule<TestClass, IEnumerable<DateTime?>, DateTime>(
                expression: x => x.DateTimesNullable,
                lower: new DateTime(1996, 01, 01),
                upper: new DateTime(2010, 01, 01))
            {
                Error = new ValidationError()
                {
                    Code = "400",
                    Message = "Test Error Message",
                    Source = "DateTimesNullable"
                }
            };

            rule.Evaluate(context);
            Assert.Single(context.Errors);
        }

        #endregion


        #region DateTime Enumerable (Non-Nullable DateTimes and Nullable IEnumerable)

        [Fact]
        public void DateTimeNullableEnumerableWithinBoundsSuccessTest()
        {
            var testClass = new TestClass()
            {
                DateTimesNullable2 = new DateTime[]
                {
                    new DateTime(1997, 01, 01),
                    new DateTime(2001, 01, 01)
                }
            };
            var context = new ValidationContext<TestClass>(testClass);
            var rule = new BetweenOrEqualToValidationRule<TestClass, IEnumerable<DateTime>?, DateTime>(
                expression: x => x.DateTimesNullable2,
                lower: new DateTime(1996, 01, 01),
                upper: new DateTime(2010, 01, 01));

            rule.Evaluate(context);
            Assert.Empty(context.Errors);
        }


        [Fact]
        public void DateTimeNullableEnumerableOutsideBoundsFailureTest()
        {
            var testClass = new TestClass();
            var context = new ValidationContext<TestClass>(testClass);
            var rule = new BetweenOrEqualToValidationRule<TestClass, IEnumerable<DateTime>?, DateTime>(
                expression: x => x.DateTimesNullable2,
                lower: new DateTime(1996, 01, 01),
                upper: new DateTime(2010, 01, 01))
            {
                Error = new ValidationError()
                {
                    Code = "400",
                    Message = "Test Error Message",
                    Source = "DateTimesNullable2"
                }
            };

            rule.Evaluate(context);
            Assert.Single(context.Errors);
        }

        #endregion


        #region DateTime Enumerable (Non-Nullable DateTime or IEnumerable)

        [Fact]
        public void DateTimeEnumerableWithinBoundsSuccessTest()
        {
            var testClass = new TestClass()
            {
                DateTimes = new DateTime[]
                {
                    new DateTime(1997, 01, 01),
                    new DateTime(2001, 01, 01)
                }
            };
            var context = new ValidationContext<TestClass>(testClass);
            var rule = new BetweenOrEqualToValidationRule<TestClass, IEnumerable<DateTime>, DateTime>(
                expression: x => x.DateTimes,
                lower: new DateTime(1996, 01, 01),
                upper: new DateTime(2010, 01, 01));

            rule.Evaluate(context);
            Assert.Empty(context.Errors);
        }


        [Fact] // DateTime failure due to outside upper bounds
        public void DateTimeEnumerableUpperBoundFailureTest()
        {
            var testClass = new TestClass()
            {
                DateTimes = new DateTime[]
                {
                    new DateTime(2010, 01, 02),
                    new DateTime(2011, 01, 01)
                }
            };
            var context = new ValidationContext<TestClass>(testClass);
            var rule = new BetweenOrEqualToValidationRule<TestClass, IEnumerable<DateTime>, DateTime>(
                expression: x => x.DateTimes,
                lower: new DateTime(1996, 01, 01),
                upper: new DateTime(2010, 01, 01))
            {
                Error = new ValidationError()
                {
                    Code = "400",
                    Message = "Test Error Message",
                    Source = "DateTimes"
                }
            };

            rule.Evaluate(context);
            Assert.Single(context.Errors);
        }


        [Fact] // DateTime failure due to outside upper bounds
        public void DateTimeEnumerableLowerBoundFailureTest()
        {
            var testClass = new TestClass()
            {
                DateTimes = new DateTime[]
                {
                    new DateTime(1995, 01, 01),
                    new DateTime(2001, 01, 01)
                }
            };
            var context = new ValidationContext<TestClass>(testClass);
            var rule = new BetweenOrEqualToValidationRule<TestClass, IEnumerable<DateTime>, DateTime>(
                expression: x => x.DateTimes,
                lower: new DateTime(1996, 01, 01),
                upper: new DateTime(2010, 01, 01))
            {
                Error = new ValidationError()
                {
                    Code = "400",
                    Message = "Test Error Message",
                    Source = "DateTimes"
                }
            };

            rule.Evaluate(context);
            Assert.Single(context.Errors);
        }

        #endregion


        #region DateTime (Nullable Tests)

        [Fact]
        public void DateTimeNullableWithinBoundsSuccessTest()
        {
            var testClass = new TestClass() { DateTimeNullable = new DateTime(1997, 01, 01) };
            var context = new ValidationContext<TestClass>(testClass);
            var rule = new BetweenOrEqualToValidationRule<TestClass, DateTime?, DateTime>(
                expression: x => x.DateTimeNullable, 
                lower: new DateTime(1996, 01, 01), 
                upper: new DateTime(2010, 01, 01));

            rule.Evaluate(context);
            Assert.Empty(context.Errors);
        }


        [Fact] // DateTime failure due to outside upper bounds
        public void DateTimeNullableUpperBoundFailureTest()
        {
            var testClass = new TestClass() { DateTimeNullable = new DateTime(2011, 01, 01) };
            var context = new ValidationContext<TestClass>(testClass);
            var rule = new BetweenOrEqualToValidationRule<TestClass, DateTime?, DateTime>(
                expression: x => x.DateTimeNullable, 
                lower: new DateTime(1996, 01, 01), 
                upper: new DateTime(2010, 01, 01))
            {
                Error = new ValidationError()
                {
                    Code = "400",
                    Message = "Test Error Message",
                    Source = "DateTimeNullable"
                }
            };

            rule.Evaluate(context);
            Assert.Single(context.Errors);
        }


        [Fact] // DateTime failure due to outside upper bounds
        public void DateTimeNullableLowerBoundFailureTest()
        {
            var testClass = new TestClass() { DateTimeNullable = new DateTime(1995, 01, 01) };
            var context = new ValidationContext<TestClass>(testClass);
            var rule = new BetweenOrEqualToValidationRule<TestClass, DateTime?, DateTime>(
                expression: x => x.DateTimeNullable,
                lower: new DateTime(1996, 01, 01),
                upper: new DateTime(2010, 01, 01))
            {
                Error = new ValidationError()
                {
                    Code = "400",
                    Message = "Test Error Message",
                    Source = "DateTimeNullable"
                }
            };

            rule.Evaluate(context);
            Assert.Single(context.Errors);
        }

        [Fact] // DateTime failure due to null property
        public void DateTimeNullableNullFailureTest()
        {
            var testClass = new TestClass() { DateTimeNullable = null };
            var context = new ValidationContext<TestClass>(testClass);
            var rule = new BetweenOrEqualToValidationRule<TestClass, DateTime?, DateTime>(
                expression: x => x.DateTimeNullable, 
                lower: new DateTime(1996, 01, 01), 
                upper: new DateTime(2010, 01, 01))
            {
                Error = new ValidationError()
                {
                    Code = "400",
                    Message = "Test Error Message",
                    Source = "DateTimeNullable"
                }
            };

            rule.Evaluate(context);
            Assert.Single(context.Errors);
        }
        #endregion


        #region DateTime (Non-Nullable Tests)

        [Fact]
        public void DateTimeWithinBoundsSuccessTest()
        {
            var testClass = new TestClass() 
            {
                DateTime = new DateTime(1997,01,01) 
            };
            var context = new ValidationContext<TestClass>(testClass);
            var rule = new BetweenOrEqualToValidationRule<TestClass, DateTime, DateTime>(
                expression: x => x.DateTime, 
                lower: new DateTime(1996,01,01), 
                upper: new DateTime(2010,01,01));

            rule.Evaluate(context);
            Assert.Empty(context.Errors);
        }


        [Fact] // DateTime failure due to outside upper bounds
        public void DateTimeUpperBoundFailureTest()
        {
            var testClass = new TestClass() 
            { 
                DateTime = new DateTime(2011, 01, 01) 
            };
            var context = new ValidationContext<TestClass>(testClass);
            var rule = new BetweenOrEqualToValidationRule<TestClass, DateTime, DateTime>(
                expression: x => x.DateTime, 
                lower: new DateTime(1996, 01, 01), 
                upper: new DateTime(2010, 01, 01))
            {
                Error = new ValidationError()
                {
                    Code = "400",
                    Message = "Test Error Message",
                    Source = "DateTime"
                }
            };

            rule.Evaluate(context);
            Assert.Single(context.Errors);
        }


        [Fact] // DateTime failure due to outside upper bounds
        public void DateTimeLowerBoundFailureTest()
        {
            var testClass = new TestClass() 
            { 
                DateTime = new DateTime(1995, 01, 01) 
            };
            var context = new ValidationContext<TestClass>(testClass);
            var rule = new BetweenOrEqualToValidationRule<TestClass, DateTime, DateTime>(
                expression: x => x.DateTime,
                lower: new DateTime(1996, 01, 01),
                upper: new DateTime(2010, 01, 01))
            {
                Error = new ValidationError()
                {
                    Code = "400",
                    Message = "Test Error Message",
                    Source = "DateTime"
                }
            };

            rule.Evaluate(context);
            Assert.Single(context.Errors);
        }

        #endregion
    }
}
