using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Assimalign.ComponentModel.ValidationTests.Rules.BetweenOrEqualTo;


public class DecimalBetweenOrEqualToValidationTests
{
    public partial class TestClass
    {
        public decimal Decimal { get; set; }
        public decimal? DecimalNullable { get; set; }
        public IEnumerable<decimal> Decimals { get; set; }
        public IEnumerable<decimal?> DecimalsNullable { get; set; }
        public IEnumerable<decimal?>? DecimalsNullable1 { get; set; }
        public IEnumerable<decimal>? DecimalsNullable2 { get; set; }
    }

}

