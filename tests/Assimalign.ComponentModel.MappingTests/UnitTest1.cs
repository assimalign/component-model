using Assimalign.ComponentModel.Mapping;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            public string MiddleName { get; set; }

            public IEnumerable<Payroll1> PayrollTransactions { get; set; }
        }

        public class Employee2
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string MiddleName { get; set; }
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


        public partial class MapperProfileTest : MapperProfile<Employee2, Employee1>
        {
            public override void Configure(IMapperActionDescriptor<Employee2, Employee1> descriptor)
            {
                descriptor
                    .BeforeMap((target, source) =>
                    {
                        source.Details ??= new EmployeeDetails();
                    })
                    .MapMembers("MiddleName", "Details.MiddleName")
                    .MapMembers(target => target.FirstName, source => source.Details.FirstName)
                    .MapMembers(target => target.LastName, source => source.Details.LastName)
                    .MapProfile(target => target.Transactions, source => source.Details.PayrollTransactions, configure =>
                    {
                        configure.MapMembers(target => target.Amount, source => source.Amount);
                    })
                    .AfterMap((target, source) =>
                    {
                        
                    });
            }
        }



        [Fact]
        public void MappingTest()
        {
            var mapper = Mapper.Create(configure =>
            {
                configure.AddProfile(new MapperProfileTest());
            });

            var employee = new Employee1()
            {
                Details = new EmployeeDetails()
                {
                    FirstName = "Chase",
                    LastName = "Crawford",
                    MiddleName = "Ryan",
                    PayrollTransactions = new Payroll1[]
                    {
                        new Payroll1()
                        {
                             Amount = 10
                        },
                        new Payroll1()
                        {
                            Amount = 12
                        }
                    }
                }
            };

            var watch = new Stopwatch();

            watch.Start();
            mapper.Map<Employee2, Employee1>(employee);
            watch.Stop();

            watch.Restart();
            var v1 = mapper.Map<Employee2, Employee1>(employee);
            watch.Stop();

            var ms = watch.ElapsedMilliseconds;
        }
    }
}
