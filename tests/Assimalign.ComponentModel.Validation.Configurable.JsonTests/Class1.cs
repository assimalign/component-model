using Xunit;
using Assimalign.ComponentModel.Validation;
using Assimalign.ComponentModel.Validation.Configurable;

namespace Assimalign.ComponentModel.Validation.Configurable.JsonTests
{
    public class Class1
    {


        [Fact]
        public void Test()
        {
            var validator = ValidationConfigBuilder.Create()
                .ConfigureJson<Person>(@"
                {
                    ""$description"": """",
                    ""$validationItems"": [
                        {
                            ""$itemMember"": ""firstName"",
                            ""$itemType"": ""Inline"",
                            ""$itemConditionId"": null,
                            ""$itemRules"": []
                        }
                    ]
                }");
        }


        public class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }



    }
}