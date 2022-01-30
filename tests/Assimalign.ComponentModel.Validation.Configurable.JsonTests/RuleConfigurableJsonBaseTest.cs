using System;

namespace Assimalign.ComponentModel.Validation.Configurable.JsonTests;


public abstract class RuleConfigurableJsonBaseTest
{
    public TestObject TestProp => new TestObject();
    public partial class TestObject
    {
        public uint UIntProp { get; set; } = 5;
        public ushort UShortProp { get; set; } = 5;
        public ulong ULongProp { get; set; } = 500000000;
        public int IntProp { get; set; } = -25;
        public short ShortProp { get; set; } = -5;
        public long LongProp { get; set; } = 5000000;
        public float FloatProp { get; set; } = 0.5f;
        public double DoubleProp { get; set; } = 5.4;
        public decimal DecimalProp { get; set; } = 0.50m;
        public DateTime DateTimeProp { get; set; } = DateTime.Now;
        public DateTimeOffset DateTimeOffsetProp { get; set; } = DateTimeOffset.Now;
        public TimeSpan TimeSpanProp { get; set; } = TimeSpan.FromTicks(DateTime.Now.Ticks);
#if NET6_0_OR_GREATER
        public DateOnly DateProp { get; set; }
        public TimeOnly TimeProp { get; set; }
#endif
        public string StringProp { get; set; }
        public Guid GuidProp { get; set; }
        public bool BooleanProp { get; set; }

    }
    public abstract void Int16SuccessTest();
    public abstract void Int16FailureTest();
    public abstract void Int32SuccessTest();
    public abstract void Int32FailureTest();
    public abstract void Int64SuccessTest();
    public abstract void Int64FailureTest();
    public abstract void UInt16SucessTest();
    public abstract void UInt16FailureTest();
    public abstract void UInt32SuccessTest();
    public abstract void UInt32FailureTest();
    public abstract void UInt64SuccessTest();
    public abstract void UInt64FailureTest();
    public abstract void SingleSuccessTest();
    public abstract void SingleFailureTest();
    public abstract void DecimalSucessTest();
    public abstract void DecimalFailureTest();
    public abstract void DoubleSuccessTest();
    public abstract void DoubleFailureTest();
    public abstract void DateTimeSuccessTest();
    public abstract void DateTimeFailureTest();
    public abstract void DateOnlySuccessTest();
    public abstract void DateOnlyFailureTest();
    public abstract void TimeSpanSuccessTest();
    public abstract void TimeSpanFailureTest();
    public abstract void TimeOnlySuccessTest();
    public abstract void TimeOnlyFailureTest();
    public abstract void StringSuccessTest();
    public abstract void StringFailureTest();
    public abstract void RecordSuccessTest();
    public abstract void RecordFailureTest();
    public abstract void GuidSuccessTest();
    public abstract void GuidFailureTest();
    public abstract void BooleanSuccessTest();
    public abstract void BooleanFailureTest();
}