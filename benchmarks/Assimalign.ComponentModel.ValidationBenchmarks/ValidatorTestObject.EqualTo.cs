using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public partial class ValidatorTestValues
{
    public const int IntEqualToValue = 10;
    public const int LongEqualToValue = 10;
    public const int ShortEqualToValue = 10;
}


public partial class ValidatorTestObject
{
    public ValidatorTestEqualTobject EqualToFields { get; set; } = new();
}


public class ValidatorTestEqualTobject
{
    public short ShortEqualToSuccessField { get; set; } = 10;
    public short ShortEqualToFailureField { get; set; } = 14;

    public int IntEqualToSuccessField { get; set; } = 10;
    public int IntEqualToFailureField { get; set; } = 14;

    public long LongEqualToSuccessField { get; set; } = 10;
    public long LongEqualToFailureField { get; set; } = 14;
}
