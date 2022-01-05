using Xunit;

using Assimalign.ComponentModel.Validation;
using Assimalign.ComponentModel.Validation.Configurable;

namespace Assimalign.ComponentModel.Validation.Configurable.XmlTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var validator = ValidationConfigBuilder.Create()
               .ConfigureXml<object>("")
               .Build();
               

        }
    }
}