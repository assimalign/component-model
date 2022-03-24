using Assimalign.ComponentModel.Mapping.Abstractions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Assimalign.ComponentModel.MappingTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var mapper = Assimalign.ComponentModel.Mapping.Mapper.Create(configure =>
            {
                configure
                    .AddProfile(new MapperProfileTest())
                    .AddProfile(new MapperProfileTest());
            });

            

        }


        public class Employee1
        {
            public EmployeeDetails Details { get; set; }
        }

        public class EmployeeDetails
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }

            public IEnumerable<Payroll1> PayrollTransactions { get; set; }
        }

        public class Employee2
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }

            public IEnumerable<Payroll2> Transactions { get; set; }
        }

        public class Payroll2
        {
            public int Amount { get; set; }
        }

        public class Payroll1
        {
            public int Amount { get; set; }
        }


        public partial class MapperProfileTest : Mapping.MapperProfile<Employee1, Employee2>
        {
            public override void Configure(IMapperProfileDescriptor<Employee1, Employee2> descriptor)
            {
                descriptor
                    .BeforeMap((source, target) =>
                    {
                        source.Details ??= new EmployeeDetails();
                    });



                descriptor.ForTarget(target => target.FirstName).MapSource(source => source.Details.FirstName.ToLower());
                descriptor.ForSource(source => source.Details.FirstName).MapTarget(target => target.FirstName.ToUpper());

                descriptor
                    .MapMembers(source => int.Parse(source.Details.FirstName), target => int.Parse(target.FirstName)) // this should cause an error
                    .MapMembers(source => source.Details.LastName, target => target.LastName);
            }
        }



        [Fact]
        public void MappingTest()
        {
            var mapper = Mapping.Mapper.Create(configure =>
            {
                configure.AddProfile(new MapperProfileTest());
            });



            var employee = new Employee1()
            {
                Details = new EmployeeDetails()
                {
                    FirstName = "Chase",
                    LastName = "Crawford"
                }
            };


            var results = mapper.Map<Employee1, Employee2>(employee);

            Assert.NotNull(results);
        }
    }
}
