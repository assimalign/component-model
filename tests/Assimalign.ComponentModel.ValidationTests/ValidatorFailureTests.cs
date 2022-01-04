﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Assimalign.ComponentModel.ValidationTests;

using Assimalign.ComponentModel.Validation;
using Assimalign.ComponentModel.Validation.Internal;
using Assimalign.ComponentModel.Validation.Internal.Rules;
using System.Text.RegularExpressions;

public class ValidatorFailureTests
{

    public class TestObject
    {
        public string FirstName { get; set; }
    }

    public class TestObjectValidationProfile : ValidationProfile<TestObject>
    {
        private Action<IValidationRuleDescriptor<TestObject>> descriptor;

        public TestObjectValidationProfile(Action<IValidationRuleDescriptor<TestObject>> descriptor)
        {
            this.descriptor = descriptor;
        }

        public override void Configure(IValidationRuleDescriptor<TestObject> descriptor)
        {
            this.descriptor.Invoke(descriptor);
        }
    }

    [Fact]
    public void SuccessValidationFailureExceptionThrownTest()
    {
        var validator = Validator.Create(configure =>
        {
            configure.ThrowExceptionOnFailure = true;

            configure.AddProfile(new TestObjectValidationProfile(descriptor =>
            {
                descriptor.RuleFor(p => p.FirstName)
                    .Matches("")
                    .NotEmpty();
            }));
        });

        Assert.Throws<ValidationFailureException>(() => validator.Validate(new TestObject()));
    }


    [Fact]
    public void SuccessValidationFailureExceptionNotThrownTest()
    {
        var validator = Validator.Create(configure =>
        {
            configure.ThrowExceptionOnFailure = true;

            configure.AddProfile(new TestObjectValidationProfile(descriptor =>
            {
                descriptor.RuleFor(p => p.FirstName)
                    .NotEmpty();
            }));
        });

        var results = validator.Validate(new TestObject() { FirstName = "Chase" });

        Assert.Empty(results.Errors);
    }
}

