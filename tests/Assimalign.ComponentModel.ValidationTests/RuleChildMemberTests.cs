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

public class RuleChildMemberTests
{
    private IValidator validator;

    public RuleChildMemberTests()
    {
        this.validator = Validator.Create(configure =>
        {
            configure.AddProfile(new PersonValidationProfile());
        });
    }


    public class PersonValidationProfile : ValidationProfile<Person>
    {
        public override void Configure(IValidationRuleDescriptor<Person> descriptor)
        {
            descriptor.RuleFor(p => p.PrimaryAddress)
                 .ChildRules(configure =>
                 {
                     configure.RuleFor(p => p.StreetOne)
                        .NotEmpty();

                     configure.RuleFor(p => p.City)
                        .NotEmpty();

                     configure.RuleFor(p => p.State)
                        .NotEmpty()
                        .Custom((value, context) =>
                        {

                        });

                     configure
                        .When(p => string.IsNullOrEmpty(p.ZipCode), op =>
                         {
                             op.RuleFor(p => p.ZipCode)
                                .NotEmpty();
                         })
                        .When(p => !string.IsNullOrEmpty(p.ZipCode), ops =>
                        {
                            ops.RuleFor(p => p.ZipCode)
                                .Custom((value, context) =>
                                {
                                    if (!int.TryParse(value, out int result))
                                    {
                                        context.AddFailure("failure", "failure");
                                    }
                                });
                        });
                 });
        }
    }


    public class Person
    {
        public PersonAddresses PrimaryAddress { get; set; }
    }

    public class PersonAddresses
    {
        public string StreetOne { get; set; }
        public string StreetTwo { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }

    [Fact]
    public void ChildRuleFailureTest01()
    {
        var person = new Person()
        {
            PrimaryAddress = new PersonAddresses()
            {
                
            }
        };

        var results = this.validator.Validate(person);
        Assert.Equal(4, results.Errors.Count());
    }

    [Fact]
    public void ChildRuleFailureTest02()
    {
        var person = new Person()
        {
            PrimaryAddress = new PersonAddresses()
            {
                StreetOne = "Test street"
            }
        };

        var results = this.validator.Validate(person);
        Assert.Equal(3, results.Errors.Count());
    }


    [Fact]
    public void ChildRuleFailureTest03()
    {
        var person = new Person()
        {
            PrimaryAddress = new PersonAddresses()
            {
                StreetOne = "Test street",
                ZipCode = "casdf"
            }
        };

        var results = this.validator.Validate(person);
        Assert.Equal(3, results.Errors.Count());
    }

    [Fact]
    public void ChildRuleFailureTest04()
    {
        var person = new Person()
        {
            PrimaryAddress = new PersonAddresses()
            {
                StreetOne = "Test street",
                ZipCode = "24242"
            }
        };

        var results = this.validator.Validate(person);
        Assert.Equal(2, results.Errors.Count());
    }


    [Fact]
    public void ChildRuleSuccessTest04()
    {
        var person = new Person()
        {
            
        };

        var results = this.validator.Validate(person);
        Assert.Empty(results.Errors);
    }
}

