using System;
using Xunit;
using System.Linq;
using System.Collections.Generic;

namespace Assimalign.ComponentModel.ValidationTests
{
    using Assimalign.ComponentModel.Validation;
    using Assimalign.ComponentModel.Validation.Rules;
    using System.Collections;

    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var user = new User() { FirstName = "Chase"};


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


            //RuleForEach(p => p.Ages)
            //    .Between(0, 34);

            //RuleFor(p => p.Details)
            //    .ChildRules(child =>
            //    {
            //        child.RuleFor(p => p.Ssn)
            //            .NotEmpty();

            //    });

            //RuleForEach(p => p.NickNames)
            //    .NotEmpty()
            //    .EmailAddress()
            //    .Custom((value, context) =>
            //    {
            //        foreach(var email in value)
            //        {

            //        }
            //    });

            



            //RuleFor(p => p.Age)
            //    //.Length(20)
                
            //    .LessThan(23);

            //RuleForEach(p => p.EmailAddress)
                
            //    .NotEmpty();


            //var v = new Nullable<int>();



            //RuleFor(p => p.FirstName)
            //    .EmailAddress()
                
            //    .Custom((value, context) =>
            //    {
            //        if (value == "Test")
            //        {
            //            context.AddValidationError()
            //        
            //    });
        }

        public override void Configure(IValidationRuleDescriptor<User> descriptor)
        {
            descriptor
                .When(x => x.EmailAddress != null, configure =>
                  {

                  })
                .Otherwise(configure =>
                {
                    
                });

            descriptor.RuleFor(x => x.Age)
                .GreaterThan(10, error =>
                {
                    error.Message = "";
                    error.Code = "400-235";
                });

            descriptor.RuleFor(p => p.Age)
                .Between(10, 12);

           // descriptor.RuleForEach(p => p.Addresses);
        }
    }



    public class User : IComparable
    {
        public int? Age { get; set; }


        public int[] Ages { get; set; }
        public string FirstName { get; set; }

        public string EmailAddress { get; set; }

        public UserDetails Details { get; set; }

        public IEnumerable<string> NickNames { get; set; }

        public IEnumerable<UserAddress> Addresses { get; set; }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
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
