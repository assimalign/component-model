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
            var validator = ValidationConfigBuilder.Create(configure =>
            {
                configure.AddConfigProvider(default);

                return configure.Build();
            });

            var profile =  builder.Build<User>().Compile();

        }
    }


    public class User
    {
        public User()
        {
            
        }
    }
}