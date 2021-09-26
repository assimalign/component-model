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
            var user = new User() { FirstName = "Chase"};
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
                validation.RuleFor(p => p.Age)
                    .Between(22, 23);
            });

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

    }


    public class User : IComparable
    {
        public int Age { get; set; }


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
