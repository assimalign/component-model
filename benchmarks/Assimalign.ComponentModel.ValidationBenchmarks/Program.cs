


var validatorBenchmarkRunTime = DateTime.Now.AddSeconds(5);
var validatorTestObject = new ValidatorTestObject();
var validatorElapsedTimerAverager = new ValidatorMovingAverage(15);
var validator = Validator.Create(configure =>
{
    configure.AddProfile(new ValidatorTestProfile());
});

while (DateTime.Now < validatorBenchmarkRunTime)
{
    var validation = validator.Validate(validatorTestObject);

    Console.SetCursorPosition(0, Console.WindowTop);
    Console.Write($"Elapsed Ticks: {validation.ValidationElapsedTicks}");

    Console.SetCursorPosition(0, Console.WindowTop + 1);
    Console.Write($"Elapsed (ms):  {validation.ValidationElapsedMilliseconds}");

    Console.SetCursorPosition(0, Console.WindowTop + 2);
    Console.Write($"Elapsed (sec): {validation.ValidationElapsedSeconds}");

    double average = 0;

    if (validation.ValidationElapsedTicks > 0)
    {
        average = validatorElapsedTimerAverager.GetAverage(validation.ValidationElapsedTicks ?? 0);
    }


    Console.SetCursorPosition(0, Console.WindowTop + 4);
    Console.Write($"Current AVG (MS): {average / (double)TimeSpan.TicksPerMillisecond}");
}

Console.SetCursorPosition(0, Console.WindowTop + 6);
Console.Write($"Current AVG (MS):   {validatorElapsedTimerAverager.CurrentAvg}");

Console.SetCursorPosition(0, Console.WindowTop + 8);
Console.Write($"Highest AVG (MS):   {validatorElapsedTimerAverager.HighestAvg}");

Console.SetCursorPosition(0, Console.WindowTop + 9);
Console.Write($"Lowest AVG (MS):    {validatorElapsedTimerAverager.LowestAvg}");

Console.SetCursorPosition(0, Console.WindowTop + 11);
Console.Write($"Highest Entry (MS): {validatorElapsedTimerAverager.HighestEntry} at {validatorElapsedTimerAverager.HighestEntryOccurance}");

Console.SetCursorPosition(0, Console.WindowTop + 12);
Console.Write($"Lowest Entry (MS):  {validatorElapsedTimerAverager.LowestEntry} at {validatorElapsedTimerAverager.LowestEntryOccurance}");

Console.SetCursorPosition(0, Console.WindowTop + 14);
Console.Write($"Total Entries:  {validatorElapsedTimerAverager.TotalEntries}");