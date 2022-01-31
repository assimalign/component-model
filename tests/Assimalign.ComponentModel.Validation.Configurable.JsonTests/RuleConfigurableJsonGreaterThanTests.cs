using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Assimalign.ComponentModel.Validation.Configurable.JsonTests;

using Assimalign.ComponentModel.Validation;
using Assimalign.ComponentModel.Validation.Configurable;



public class RuleConfigurableJsonGreaterThanTests : RuleConfigurableJsonBaseTest
{
    private readonly IValidator failure;
    private readonly IValidator success;

    public RuleConfigurableJsonGreaterThanTests()
    {
        var configSuccess = File.ReadAllText("ConfigRules/ConfigRule.Success.json");
        var configFailure = File.ReadAllText("ConfigRules/ConfigRule.Failures.json");

        success = ValidationConfigurableBuilder.Create()
           .AddJsonSource<TestObject>(configSuccess)
           .Build()
           .ToValidator();

        failure = ValidationConfigurableBuilder.Create()
           .AddJsonSource<TestObject>(configFailure)
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

    [Fact]
    public override void DoubleSuccessTest()
    {

        var results = success.Validate(new TestObject()
        {
            GuidProp = new Guid("567bf15a-1d82-ec11-b77c-000d3a19155f")
        });
        Assert.DoesNotContain(results.Errors, x => x.Code == "guid.gt.failure");

    }

    [Fact]
    public override void GuidFailureTest()
    {
        var results = failure.Validate(new TestObject()
        {
            GuidProp = new Guid("557bf15a-1d82-ec11-b77c-000d3a19155f")
        });
        Assert.Contains(results.Errors, x => x.Code == "guid.gt.failure");
    }

    public override void GuidSuccessTest()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public override void Int16FailureTest()
    {
        var results = failure.Validate(base.TestProp);
        Assert.Contains(results.Errors, x => x.Code == "short.gt.failure");
    }

    [Fact]
    public override void Int16SuccessTest()
    {
        var results = success.Validate(base.TestProp);
        Assert.Empty(results.Errors);
    }

    [Fact]
    public override void Int32FailureTest()
    {
        var results = failure.Validate(base.TestProp);
        Assert.Contains(results.Errors, x => x.Code == "int.gt.failure");

    }

    [Fact]
    public override void Int32SuccessTest()
    {
        var results = success.Validate(base.TestProp);
        Assert.Empty(results.Errors);
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

    [Fact]
    public override void UInt16FailureTest()
    {
        var results = failure.Validate(base.TestProp);
        Assert.Contains(results.Errors, x => x.Code == "ushort.gt.failure");
    }

    public override void UInt16SucessTest()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public override void UInt32FailureTest()
    {
        var results = failure.Validate(base.TestProp);
        Assert.Contains(results.Errors, x => x.Code == "uint.gt.failure");
    }

    public override void UInt32SuccessTest()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public override void UInt64FailureTest()
    {
        var results = failure.Validate(base.TestProp);
        Assert.Contains(results.Errors, x => x.Code == "ulong.gt.failure");
    }

    public override void UInt64SuccessTest()
    {
        throw new NotImplementedException();
    }
}

