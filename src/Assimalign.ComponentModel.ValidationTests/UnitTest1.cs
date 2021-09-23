using System;
using Xunit;
using System.Linq;
using System.Collections.Generic;

namespace Assimalign.ComponentModel.ValidationTests
{
    using Assimalign.ComponentModel.Validation;
    using Assimalign.ComponentModel.Validation.Abstraction;
    using Assimalign.ComponentModel.Validation.Rules;
    

    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var user = new User() { EmailAddress = "" };
            var validator = new UserValidator();
            var validation = validator.Validate(user);
        }
    }

    public class UserValidator : Validator<User>
    {

        public UserValidator()
        {

            When(x => x.FirstName == "Chase", validation =>
            {
                validation.RuleFor(p => p.NickNames)
                    .NotEmpty();



                validation.RuleForEach(p => p.Addresses)
                    .NotEmpty();

            });

            RuleForEach(p => p.NickNames)
                .NotEmpty()
                .EmailAddress("Test message to apply to the email failure.", "4556")
                .Custom((value, context) =>
                {
                    foreach(var email in value)
                    {

                    }
                });

            RuleFor(p => p.Age)
                .LessThan(0);

            RuleForEach(p => p.EmailAddress)
                .NotEmpty();


            var v = new Nullable<int>();



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

    }


    public class User
    {
        public int? Age { get; set; }
        public string FirstName { get; set; }

        public string EmailAddress { get; set; }

        public IEnumerable<string> NickNames { get; set; }

        public IEnumerable<UserAddress> Addresses { get; set; }
    }

    public class UserAddress
    {
        public string StreetOne { get; set; }
    }
}
