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
    public partial class MapperMemberToMemberProfile : MapperProfile<Person1, Person2>
    {
        public override void Configure(IMapperActionDescriptor<Person1, Person2> descriptor)
        {
            descriptor
                .MapTarget(target => target.Age, source => source.Details.Age.ToNullable())
                .MapTarget(target => target.FirstName, source => source.Details.FirstName)
                .MapTarget(target => target.LastName, source => source.Details.LastName)
                .MapTarget("MiddleName", "Details.MiddleName")
                .MapTarget(target => target.Following, source => source.Details.Following.ToDictionary(key => key.Id, value => new Person1()
                {
                    FirstName = value.FirstName
                }));
        }
    }

    [Fact]
    public void RunMemberToMemberTest()
    {
        var mapper = Mapper.Create(configure =>
        {
            configure.AddProfile(new MapperMemberToMemberProfile());
        });

        var person2 = new Person2()
        {
            Details = new Person2Details()
            {
                FirstName = "Chase",
                LastName = "Crawford",
                MiddleName = "Ryan"
            }
        };

        var person1 = mapper.Map<Person1, Person2>(person2);

        Assert.Equal("Chase", person1.FirstName);
        Assert.Equal("Crawford", person1.LastName);
        Assert.Equal("Ryan", person1.MiddleName);

    }
}
