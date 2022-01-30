using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Assimalign.ComponentModel.Validation.Configurable.JsonTests
{
    using Assimalign.ComponentModel.Validation;
    using Assimalign.ComponentModel.Validation.Configurable;
    

    public class RuleConfigurableJsonGreaterThanTests : RuleConfigurableJsonBaseTest
    {
        private readonly IValidator failurValidator;
        private readonly IValidator successValidator;

        public RuleConfigurableJsonGreaterThanTests()
        {
            var config = File.ReadAllText("ConfigRules/GreaterThanConfig.Success.json");
            successValidator = ValidationConfigurableBuilder.Create()
               .AddJsonSource<TestObject>(config)
               .Build()
               .ToValidator();
        }

        public override void BooleanFailureTest()
        {
            throw new NotImplementedException();
        }

        public override void BooleanSuccessTest()
        {
            throw new NotImplementedException();
        }

        public override void DateOnlyFailureTest()
        {
            throw new NotImplementedException();
        }

        public override void DateOnlySuccessTest()
        {
            throw new NotImplementedException();
        }

        public override void DateTimeFailureTest()
        {
            throw new NotImplementedException();
        }

        public override void DateTimeSuccessTest()
        {
            throw new NotImplementedException();
        }

        public override void DecimalFailureTest()
        {
            throw new NotImplementedException();
        }

        public override void DecimalSucessTest()
        {
            throw new NotImplementedException();
        }

        public override void DoubleFailureTest()
        {
            throw new NotImplementedException();
        }

        public override void DoubleSuccessTest()
        {
            throw new NotImplementedException();
        }

        public override void GuidFailureTest()
        {
            throw new NotImplementedException();
        }

        public override void GuidSuccessTest()
        {
            throw new NotImplementedException();
        }

        public override void Int16FailureTest()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public override void Int16SuccessTest()
        {
            var results = successValidator.Validate(base.TestProp);
            Assert.Empty(results.Errors);
        }

        public override void Int32FailureTest()
        {
            throw new NotImplementedException();
        }

        public override void Int32SuccessTest()
        {
            throw new NotImplementedException();
        }

        public override void Int64FailureTest()
        {
            throw new NotImplementedException();
        }

        public override void Int64SuccessTest()
        {
            throw new NotImplementedException();
        }

        public override void RecordFailureTest()
        {
            throw new NotImplementedException();
        }

        public override void RecordSuccessTest()
        {
            throw new NotImplementedException();
        }

        public override void SingleFailureTest()
        {
            throw new NotImplementedException();
        }

        public override void SingleSuccessTest()
        {
            throw new NotImplementedException();
        }

        public override void StringFailureTest()
        {
            throw new NotImplementedException();
        }

        public override void StringSuccessTest()
        {
            throw new NotImplementedException();
        }

        public override void TimeOnlyFailureTest()
        {
            throw new NotImplementedException();
        }

        public override void TimeOnlySuccessTest()
        {
            throw new NotImplementedException();
        }

        public override void TimeSpanFailureTest()
        {
            throw new NotImplementedException();
        }

        public override void TimeSpanSuccessTest()
        {
            throw new NotImplementedException();
        }

        public override void UInt16FailureTest()
        {
            throw new NotImplementedException();
        }

        public override void UInt16SucessTest()
        {
            throw new NotImplementedException();
        }

        public override void UInt32FailureTest()
        {
            throw new NotImplementedException();
        }

        public override void UInt32SuccessTest()
        {
            throw new NotImplementedException();
        }

        public override void UInt64FailureTest()
        {
            throw new NotImplementedException();
        }

        public override void UInt64SuccessTest()
        {
            throw new NotImplementedException();
        }
    }
}
