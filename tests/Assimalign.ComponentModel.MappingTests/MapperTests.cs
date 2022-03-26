using Assimalign.ComponentModel.Mapping;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;

namespace Assimalign.ComponentModel.MappingTests;

public partial class MapperTests
{
    public class Person1
    {
        public int? Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public IDictionary<string, Person1> Following { get; set; } // id:  person
    }




    public class Person2
    {
        public Person2Details Details { get; set; }
    }
    public class Person2Details
    {
        public int Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public IEnumerable<Person2Following> Following { get; set; }
    }
    public class Person2Following
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
    }




    public partial class MapperProfileTest : MapperProfile<Person1, Person2Details>
    {
        public override void Configure(IMapperActionDescriptor<Person1, Person2Details> descriptor)
        {
            descriptor
                .MapAllProperties()
                .MapTarget(target => target.FirstName, source => source.FirstName);
        }
    }



    [Fact]
    public void MappingTest()
    {
        var mapper = Mapper.Create(configure =>
        {
            configure.AddProfile(new MapperProfileTest());
        });

       

    }
}
