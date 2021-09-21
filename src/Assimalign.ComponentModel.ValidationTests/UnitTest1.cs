using System;
using Xunit;

namespace Assimalign.ComponentModel.ValidationTests
{
    using Assimalign.ComponentModel.Validation;
    using Assimalign.ComponentModel.Validation.Rules;
    using System.Collections.Generic;

    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

        }
    }

    public class UserValidator : Validator<User>
    {

        public UserValidator()
        {

            When(x => x.FirstName == "Chase", validate =>
              {
                  validate.RuleFor(p => p.NickNames)
                  .EmailAddress();
  
              });

            RuleForEach(p => p.NickNames);
                


            RuleFor(p => p.FirstName)
                .EmailAddress()
                
                .Custom((value, context) =>
                {
                    if (value == "Test")
                    {
                        context.AddValidationError()
                    }
                });
        }

    }


    public class User
    {
        public int Age { get; set; }
        public string FirstName { get; set; }

        public IEnumerable<string> NickNames { get; set; }
    }
}
