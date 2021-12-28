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
            var user = new User() { FirstName = "Chase", Age = 11, Record = new TestRecord() { FirstName = "Chases" } };


            //Assert.That()
            var validator = Validator.Create(configure =>
            {
                configure.AddProfile(new UserValidationProfile());
            });

            var validation = validator.Validate(user);
        }
    }

    public class UserValidationProfile : ValidationProfile<User>
    {

        public UserValidationProfile()
        {

        }

        public override void Configure(IValidationRuleDescriptor<User> descriptor)
        {

            descriptor.RuleForEach(p => p.NickNames)
                .EmailAddress()
                .Length(1,2, configure => { })
                .EqualTo(new User())
                .Custom((user, context) =>
                {
                    
                });
           
            descriptor.RuleFor(p => p.Record)
                .EqualTo(new TestRecord() { FirstName = "Chase" });

            descriptor.RuleFor(p => p.FirstName)
                .EmailAddress()
                .Length(0, 9);
               // .MaxLength(10);

            descriptor.RuleFor(p => p.Age)
                .GreaterThanOrEqualTo(0)
                .EqualTo(11);

           // descriptor.RuleForEach(p => p.Addresses);
        }
    }


    public class User : IComparable
    {
        public long? Age { get; set; }

        public TestRecord Record { get; set; }

        public int[] Ages { get; set; }
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
