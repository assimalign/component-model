using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Assimalign.ComponentModel.ValidationTests;

using Assimalign.ComponentModel.Validation;
using Assimalign.ComponentModel.Validation.Internal;
using Assimalign.ComponentModel.Validation.Internal.Rules;


public class RuleEqualToTests : RuleBaseTest
{

    public IValidationContext RunEqualToTest<TValue>(TValue testValue, TValue comparisonValue)
    {
        var rule = new EqualToValidationRule<TValue>(comparisonValue)
        {
            Error = new ValidationError()
            {

            }
        };

        if (rule.TryValidate(testValue, out var context))
        {
            return context;
        }
        else
        {
            throw new Exception();
        }
    }

    [Fact]
    public override void BooleanFailureTest()
    {
        var context = this.RunEqualToTest(true, false);
        Assert.Single(context.Errors);
    }

    [Fact]
    public override void BooleanSuccessTest()
    {
        var context = this.RunEqualToTest(true, true);
        Assert.Empty(context.Errors);
    }

    public override void DateOnlyFailureTest()
    {
        throw new NotImplementedException();
    }

    public override void DateOnlySuccessTest()
    {
        throw new NotImplementedException();
    }

    public override void DateTimeFilaureTest()
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

    public override void DoubleFaiureTest()
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

    public override void Int16SuccessTest()
    {
        throw new NotImplementedException();
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

