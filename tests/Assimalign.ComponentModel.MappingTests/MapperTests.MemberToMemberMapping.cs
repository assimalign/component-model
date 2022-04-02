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
                .MapMember(target => target.Age, source => source.Details.Age)
                .MapMember(target => target.Birthdate, source => source.Details.Birthdate.GetValueOrDefault())
                .MapMember(target => target.FirstName, source => source.Details.FirstName)
                .MapMember(target => target.LastName, source => source.Details.LastName)
                .MapMember("MiddleName", "Details.MiddleName")
                .MapMember(target => target.Following, source => source.Details.Following.ToDictionary(key => key.Id, value => new Person1()
                {
                    FirstName = value.FirstName
                }))
                
                .MapProfile(target => target.PrimaryAddress, source => source.Details.PrimaryAddress, descriptor =>
                  {
                      descriptor.MapAllProperties();
                  });
        }
    }

    //public partial class MapperMemberToMember1Profile : MapperProfile<Person2Details, Person1>
    //{
    //    public override void Configure(IMapperActionDescriptor<Person2Details, Person1> descriptor)
    //    {
    //        descriptor
    //            .MapProfile(target => target.Following, source => source.Following, descripto =>
    //            {
    //                descripto
    //                    .MapMember(target => target.FirstName, source => source.Value.FirstName)
    //                    .MapAllFields();
    //            });
    //    }
    //}

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
                Age = 12,
                FirstName = "Chase",
                LastName = "Crawford",
                MiddleName = "Ryan",
                Following = new[]
                {
                    new Person2Following()
                    {
                        Id = "cbowers",
                        FirstName = "Charles",
                        LastName = "Bowers"
                    }
                },
                PrimaryAddress = new Person2Address()
                {
                    StreetOne = "1010 Kenilworth Ave"
                }
            }
        };

        var person1 = mapper.Map<Person1, Person2>(person2);

        Assert.Equal("Chase", person1.FirstName);
        Assert.Equal("Crawford", person1.LastName);
        Assert.Equal("Ryan", person1.MiddleName);

    }
}
