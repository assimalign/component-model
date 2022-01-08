using Xunit;

using Assimalign.ComponentModel.Validation.Configurable;

using Assimalign.ComponentModel.Validation;

namespace Assimalign.ComponentModel.Validation.ConfigurableTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //var factory = ValidatorFactory.Configure(configure =>
            //{
            //    configure.AddValidator("scenario 1", options =>
            //    {
            //        options.AddProfile(Loans)
            //    });

            //    configure.AddValidator<Person>(condition => )
            //});
            var validator = ValidationConfigurableBuilder.Create()
                .Configure(source =>
                {
                    return source.Build();
                })
                .Configure(configure =>
                {
                    return configure.Build();
                })
                .Build();


           


            

            

        }
    }

    public class Person
    {

    }


    public class User
    {
        public User()
        {
            
        }
    }
}