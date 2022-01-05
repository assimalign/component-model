
using Assimalign.ComponentModel.Validation;
using Assimalign.ComponentModel.Validation.Configurable;

namespace Assimalign.ComponentModel.Validation.Configurable.JsonTests
{
    public class Class1
    {

        public Class1()
        {
            var validator = ValidationConfigBuilder.Create()
                .ConfigureJson("");
        }



    }
}