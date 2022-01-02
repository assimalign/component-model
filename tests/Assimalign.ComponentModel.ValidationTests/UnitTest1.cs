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
            var user = new User() { FirstName = "Chases", Ages = new List<long?>() { 11, null }, Age = 12, Record = new TestRecord() { FirstName = "Chase" } };


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
            //base.Name = "test"
        }

        public override void Configure(IValidationRuleDescriptor<User> descriptor)
        {
            //Half half = (Half)23.0;
            //descriptor.RuleForEach(p => p.Addresses);

            //descriptor.RuleFor(p => p.FirstName)
            //    .EmailAddress()
            //    .NotEqualTo("Chase");

            //descriptor.RuleFor(p => p.Record)
            //    .Null()
            //    .EqualTo(new TestRecord() { FirstName = "Chase" });

            //descriptor.RuleFor(p => p.FirstName).NotEmpty()
            //    .MinLength(2)
            //    .Length(0, 9);

            //descriptor.RuleForEach(p => p.Ages)
            //    .EqualTo(half);


            descriptor.RuleForEach(p => p.Addresses)
                .ChildRules(configure =>
                {
                    configure.RuleFor(p => p.StreetOne);
                });

            descriptor.RuleFor(p => p.Record)
                .ChildRules(conifgure =>
                {
                    conifgure.RuleFor(p => p.FirstName);
                });

            descriptor.RuleFor(p => p.Ages)
                .Length(3)

                .Length(2,5)
                .MaxLength(0);

            descriptor.RuleFor(p=>p.Record)
                .NotNull()
                .EqualTo(new TestRecord() { FirstName = "Chase" });

            descriptor
                .When(p => p.Age > 10, configure =>
                  {
                      configure.RuleFor(p => p.Age)
                        .Between(10, 99)
                        .NotEqualTo(5);

                      configure.RuleFor(p => p.FirstName)
                        .NotEmpty()
                        .EqualTo("Chase");
                  })
                .When(p => p.Age < 10, configure =>
                 {
                     configure.RuleFor(p => p.FirstName)
                           .EqualTo("NotChase");
                 });
            
            
           // descriptor.RuleForEach(p => p.Addresses);
        }
    }

    public enum UserGender
    {
        Male = 0,
        Female = 1
    }


    public class User : IComparable
    {
        public UserGender Gender { get; set; }
        public long? Age { get; set; }

        public TestRecord? Record { get; set; }

        public long[]? Ages { get; set; }
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
