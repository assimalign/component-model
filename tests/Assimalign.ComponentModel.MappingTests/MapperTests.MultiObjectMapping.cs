using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Assimalign.ComponentModel.MappingTests;

using Assimalign.ComponentModel.Mapping;


public partial class MapperTests
{

    public class PersonTarget
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }

    public class PersonSource1
    {
        public string FirstName { get; set; }
    }

    public class PersonSource2
    {
        public string LastName { get; set; }
        public int? Age { get; set; }
    }

    public class PersonProfile1 : MapperProfile<PersonTarget, PersonSource1>
    {
        public override void Configure(IMapperActionDescriptor<PersonTarget, PersonSource1> descriptor)
        {
            descriptor
                .MapMember(target => target.FirstName, source => source.FirstName);
        }
    }
    public class PersonProfile2 : MapperProfile<PersonTarget, PersonSource2>
    {
        public override void Configure(IMapperActionDescriptor<PersonTarget, PersonSource2> descriptor)
        {
            descriptor
                .MapMember(target => target.LastName, source => source.LastName)
                .MapMember(target => target.Age, source => source.Age.GetValueOrDefault());
        }
    }

    [Fact]
    public void TestMulitObjectMap()
    {
        var mapper = Mapper.Create(configure =>
        {
            configure
                .AddProfile(new PersonProfile1())
                .AddProfile(new PersonProfile2());
        });

        var s1 = new PersonSource1() { FirstName = "Chase" };
        var s2 = new PersonSource2() { LastName = "Crawford", Age = 25 };

        var results =  mapper.Map<PersonTarget>(s1, s2);

        Assert.Equal("Chase", results.FirstName);
        Assert.Equal("Crawford", results.LastName);
    }





}
