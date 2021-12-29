using System;
using Xunit;
using Xunit.Extensions;
using System.Linq;
using System.Collections.Generic;

namespace Assimalign.ComponentModel.ValidationTests
{
    using Assimalign.ComponentModel.Validation;
    using Assimalign.ComponentModel.Validation.Internal.Rules;
    using System.Collections;

    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var user = new User() { FirstName = "Chase", Ages = new List<long?>() { 11, null }, Record = new TestRecord() { FirstName = "Chase" } };


            //Assert.That()
            var validator = Validator.Create(configure =>
            {
                configure.AddProfile(new UserValidationProfile());
                configure.AddProfile(new UserValidationProfile());
            });

            var validatorFactory = ValidatorFactory.Configure(configure =>
            {
                configure.AddValidator("scenario 1", configure =>
                {
                    configure.AddProfile(new UserValidationProfile());
                });

                configure.AddValidator("scenario 2", configure =>
                {
                    configure.AddProfile(new UserValidationProfile());
                });
            });


            var validator1 = validatorFactory.Create("scenario 2");

            var validation = validator.Validate(user);
        }
    }

    public class UserValidationProfile : ValidationProfile<User>
    {

        public UserValidationProfile()
        {
            //base.Name = "test"
        }

        public override void Configure(IValidationRuleDescriptor<User> descriptor)
        {
            Half half = (Half)23.0;
            descriptor.RuleFor(p => p.NickNames)
                .EqualTo(10)
                .NotEmpty();

            descriptor.RuleFor(p => p.Record)
                .EqualTo(new TestRecord() { FirstName = "Chase" });

            descriptor.RuleFor(p => p.FirstName)
                .Length(0, 9);

            descriptor.RuleForEach(p => p.Ages)
                .EqualTo(half);

            
           // descriptor.RuleForEach(p => p.Addresses);
        }
    }


    public class User : IComparable
    {
        public int Age { get; set; }

        public TestRecord? Record { get; set; }

        public IEnumerable<long?> Ages { get; set; }
        public string FirstName { get; set; }

        public string EmailAddress { get; set; }

        public UserDetails Details { get; set; }

        public IList<string> NickNames { get; set; }

        public IEnumerable<UserAddress>? Addresses { get; set; }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }

    public record TestRecord
    {
        public string FirstName { get; set; }
    }
    public class UserDetails
    {
        public string Ssn { get; set; }
    }

    public class UserAddress
    {
        public string StreetOne { get; set; }
    }
}
