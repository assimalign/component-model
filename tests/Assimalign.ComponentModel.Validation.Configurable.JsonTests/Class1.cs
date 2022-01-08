using Xunit;
using Assimalign.ComponentModel.Validation;
using Assimalign.ComponentModel.Validation.Configurable;
using System.Linq;

namespace Assimalign.ComponentModel.Validation.Configurable.JsonTests
{
    public class Class1
    {


        [Fact]
        public void Test()
        {
            var validator = ValidationConfigurableBuilder.Create()
                .AddJsonSource<Person>(@"
                {
                    ""$description"": """",
                    ""$validationItems"": [
                        {
                            ""$itemMember"": ""firstName"",
                            ""$itemType"": ""Inline"",
                            ""$itemRules"": [
                                {
                                    ""$rule"": ""EqualTo""
                                },
                                {
                                    ""$rule"": ""NotEqualTo""
                                },
                                {
                                    ""$rule"": ""NotEmpty""
                                }
                            ]
                        }
                    ]
                }")
                .Build()
                .ToValidator(); 


        }


        public class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }



    }
}