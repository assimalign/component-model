using Assimalign.ComponentModel.Validation;

var end = DateTime.Now.AddSeconds(20);
var user = new User() { FirstName = "Chases", Ages = new List<long?>() { 11, null }, Age = 12, Record = new TestRecord() { FirstName = "Chase" } };
var averager = new MovingAverage(15);

//Assert.That()
var validator = Validator.Create(configure =>
{
    configure.AddProfile(new UserValidationProfile());
});

while (DateTime.Now < end)
{
    var validation = validator.Validate(user);

    //Console.SetCursorPosition(0, Console.WindowTop);
    //Console.Write($"Elapsed Ticks: {validation.ValidationElapsedTicks}");

    //Console.SetCursorPosition(0, Console.WindowTop + 1);
    //Console.Write($"Elapsed (ms):  {validation.ValidationElapsedMilliseconds}");

    //Console.SetCursorPosition(0, Console.WindowTop + 2);
    //Console.Write($"Elapsed (sec): {validation.ValidationElapsedSeconds}");

    double average = 0;

    if (validation.ValidationElapsedTicks > 0)
    {
        average = averager.GetAverage(validation.ValidationElapsedTicks ?? 0);
    }


    //Console.SetCursorPosition(0, Console.WindowTop + 4);
    //Console.Write($"Current AVG (MS): {average /(double)TimeSpan.TicksPerMillisecond}");
}

Console.SetCursorPosition(0, Console.WindowTop + 6);
Console.Write($"Current AVG (MS):   {averager.CurrentAvg}");

Console.SetCursorPosition(0, Console.WindowTop + 8);
Console.Write($"Highest AVG (MS):   {averager.HighestAvg}");

Console.SetCursorPosition(0, Console.WindowTop + 9);
Console.Write($"Lowest AVG (MS):    {averager.LowestAvg}");

Console.SetCursorPosition(0, Console.WindowTop + 11);
Console.Write($"Highest Entry (MS): {averager.HighestEntry} at {averager.HighestEntryOccurance}");

Console.SetCursorPosition(0, Console.WindowTop + 12);
Console.Write($"Lowest Entry (MS):  {averager.LowestEntry} at {averager.LowestEntryOccurance}");

Console.SetCursorPosition(0, Console.WindowTop + 14);
Console.Write($"Total Entries:  {averager.TotalEntries}");


public class MovingAverage
{
    private readonly int avgSize;
    private readonly Queue<double> entries;

    private double lowestEntry;
    private double highestEntry;
    private double lowestAvg;
    private double highestAvg;
    private double acummulation;
    private double current;
    
    private long count;
    private long highestEntryOccurance;
    private long lowestEntryOccurance;

    public MovingAverage(int avgSize)
    {
        this.count = 0;
        this.avgSize = avgSize;
        this.entries = new Queue<double>();
    }

    private bool isInitialized;

    public long LowestEntryOccurance => this.lowestEntryOccurance;
    public long HighestEntryOccurance => this.highestEntryOccurance;

    public double LowestAvg => lowestAvg / (double)TimeSpan.TicksPerMillisecond;
    public double HighestAvg => highestAvg / (double)TimeSpan.TicksPerMillisecond;
    public double LowestEntry => lowestEntry / (double)TimeSpan.TicksPerMillisecond;
    public double HighestEntry => highestEntry / (double)TimeSpan.TicksPerMillisecond;
    public double CurrentAvg => current / (double)TimeSpan.TicksPerMillisecond;

    public long TotalEntries => count;

    public double GetAverage(double entry)
    {
        if (!isInitialized)
        {
            lowestEntry = entry;
            highestEntry = entry;
            highestEntryOccurance = count;
            lowestEntryOccurance = count;
        }
        else
        {
            if (entry < lowestEntry)
            {
                lowestEntry = entry;
                lowestEntryOccurance = count;
            }
            else if (entry > highestEntry)
            {
                highestEntry = entry;
                highestEntryOccurance = count;
            }
        }

        acummulation += entry;
        entries.Enqueue(entry);

        if (entries.Count >= avgSize)
        {
            acummulation -= entries.Dequeue();
        }

        current = acummulation / (double)entries.Count;

        if (!isInitialized)
        {
            lowestAvg = entry;
            highestAvg = entry;
            isInitialized = true;
        }
        else
        {
            if (current < lowestAvg)
            {
                lowestAvg = current;
            }
            if (current > highestAvg)
            {
                highestAvg = current;
            }
        }

        count++;
        return current;
    }
}


public class UserValidationProfile : ValidationProfile<User>
{

    public UserValidationProfile()
    {
        //base.Name = "test"
    }

    public override void Configure(IValidationRuleDescriptor<User> descriptor)
    {
        //Half half = (Half)23.0;
        //descriptor.RuleForEach(p => p.Addresses);

        //descriptor.RuleFor(p => p.FirstName)
        //    .EmailAddress()
        //    .NotEqualTo("Chase");

        //descriptor.RuleFor(p => p.Record)
        //    .Null()
        //    .EqualTo(new TestRecord() { FirstName = "Chase" });

        //descriptor.RuleFor(p => p.FirstName).NotEmpty()
        //    .MinLength(2)
        //    .Length(0, 9);

        //descriptor.RuleForEach(p => p.Ages)
        //    .EqualTo(half);


        descriptor.RuleFor(p => p.Record)
            .EqualTo(new TestRecord() { FirstName = "Chase" });

        descriptor
            .When(p => p.Age > 10, configure =>
            {
                configure.RuleFor(p => p.FirstName)
                        .EqualTo("Chase");
            })
            .When(p => p.Age < 10, configure =>
            {
                configure.RuleFor(p => p.FirstName)
                          .EqualTo("NotChase");
            });


        // descriptor.RuleForEach(p => p.Addresses);
    }
}


public class User : IComparable
{
    public int Age { get; set; }

    public TestRecord? Record { get; set; }

    public IEnumerable<long?> Ages { get; set; }
    public string FirstName { get; set; }

    public string EmailAddress { get; set; }

    public UserDetails Details { get; set; }

    public IList<string> NickNames { get; set; }

    public IEnumerable<UserAddress>? Addresses { get; set; }

    public int CompareTo(object obj)
    {
        throw new NotImplementedException();
    }
}

public record TestRecord
{
    public string FirstName { get; set; }
}
public class UserDetails
{
    public string Ssn { get; set; }
}

public class UserAddress
{
    public string StreetOne { get; set; }
}
