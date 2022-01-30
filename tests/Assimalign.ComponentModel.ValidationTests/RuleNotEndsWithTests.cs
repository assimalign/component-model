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


public class RuleNotEndsWithTests
{
    public IValidationContext RunNotEndsWithTest(string value, string input, StringComparison comparison = StringComparison.InvariantCulture)
    {
        var rule = new MustNotEndWithValidationRule(value, comparison)
        {
            Error = new ValidationError()
            {

            }
        };

        if (rule.TryValidate((object)input, out var context))
        {
            return context;
        }
        else
        {
            throw new Exception("Unable to validate.");
        }
    }

    [Fact]
    public void StringSuccessNotEndsWithCaseInsensitive()
    {
        var context = RunNotEndsWithTest("crawfor", "Chase Crawford", StringComparison.InvariantCultureIgnoreCase);

        Assert.Empty(context.Errors);
    }

    [Fact]
    public void StringSuccessNotEndsWithCaseSensitive()
    {
        var context = RunNotEndsWithTest("crawford", "Chase Crawford");

        Assert.Empty(context.Errors);
    }

    [Fact]
    public void StringFailureNotEndsWithCaseInsensitive()
    {
        var context = RunNotEndsWithTest("crawford", "Chase Crawford", StringComparison.InvariantCultureIgnoreCase);

        Assert.Single(context.Errors);
    }

    [Fact]
    public void StringFailureNotEndsWithCaseSensitive()
    {
        var context = RunNotEndsWithTest("Crawford", "Chase Crawford");

        Assert.Single(context.Errors);
    }
}