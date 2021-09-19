using System;
using Xunit;

namespace Assimalign.ComponentModel.ValidationTests
{
    using Assimalign.ComponentModel.Validation;
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
            RuleForEach(p=>p.NickNames)
            
            RuleFor(p => p.FirstName)
                .
        }

    }


    public class User
    {
        public string FirstName { get; set; }

        public IEnumerable<string> NickNames { get; set; }
    }
}
