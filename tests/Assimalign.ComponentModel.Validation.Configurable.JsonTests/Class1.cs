using Xunit;
using System;
using Assimalign.ComponentModel.Validation;
using Assimalign.ComponentModel.Validation.Configurable;
using System.Linq;
using System.Collections.Generic;

namespace Assimalign.ComponentModel.Validation.Configurable.JsonTests
{
    public class Class1
    {


        [Fact]
        public void Test()
        {

            var profile = new ValidationConfigurableJsonProfile<Person>();



            var validator = ValidationConfigurableBuilder.Create()
                .AddJsonSource<Person>(@"
                {
                    ""$description"": ""This is a test"",
                    ""$validationConditions"": [
                        {
                            ""$condition"": {
                                ""$member"": ""Age"",
                                ""$operator"": ""eq"",
                                ""$value"": 20
                            },
                            ""$validationItems"": [
                                 {
                                    ""$itemMember"": ""Addresses"",
                                    ""$itemType"": ""Inline"",
                                    ""$itemRules"": [
                                        {
                                            ""$rule"": ""NotEmpty""
                                        },
                                        {
                                            ""$rule"": ""LengthMax"",
                                            ""$max"": 25 
                                        }
                                    ]
                                }
                            ]
                        }
                    ],
                    ""$validationItems"": [
                        {
                            ""$itemMember"": ""firstName"",
                            ""$itemType"": ""Inline"",
                            ""$itemRules"": [
                                {
                                    ""$rule"": ""NotEmpty"",
                                    ""$error"": {
                                        ""$message"": ""The following property 'firstName' cannot be empty."",
                                        ""$code"": ""400.001""
                                    }
                                },
                                {
                                    ""$rule"": ""EqualTo"",
                                    ""$value"": ""Chase""
                                }
                            ]
                        },
                        {
                            ""$itemMember"": ""Addresses"",
                            ""$itemType"": ""Recursive"",
                            ""$itemRules"": [
                                {
                                    ""$rule"": ""Child"",
                                    ""$validationItems"": [
                                        {
                                            ""$itemMember"": ""StreetOne"",
                                            ""$itemConditionId"": null,
                                            ""$itemType"": ""inline"",
                                            ""$itemRules"": [
                                                {
                                                    ""$rule"": ""NotEmpty""
                                                }
                                            ]
                                        }
                                    ]
                                }
                            ]
                        }
                    ]
                }")
                .Build()
                .ToValidator();


            var results = validator.Validate(new Person()
            {
                FirstName = "Crawford",
                Age = 20,
               
            });

        }


        public class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int? Age { get; set; }
            public IEnumerable<PersonAddress> Addresses { get; set; }
        }

        public class PersonAddress
        {
            public string StreetOne { get; set; }
            public string StreetTwo { get; set; }
            public string City { get; set; }
            public string ZipCode { get; set; }
        }
    }
}