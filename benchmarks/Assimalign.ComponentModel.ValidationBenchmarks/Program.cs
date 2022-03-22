using static System.Console;


var validatorBenchmarkRunTime = DateTime.Now.AddSeconds(20);
var validatorTestObject = new ValidatorTestObject();
var validatorElapsedTimerAverager = new ValidatorMovingAverage(15);
var validator = Validator.Create(configure =>
{
    configure.AddProfile(new ValidatorTestProfile());
});

while (DateTime.Now < validatorBenchmarkRunTime)
{
    var validation = validator.Validate(validatorTestObject);

    //SetCursorPosition(0, WindowTop);
    //Write($"Elapsed Ticks: {validation.ValidationElapsedTicks}");

    //SetCursorPosition(0, WindowTop + 1);
    //Write($"Elapsed (sec): {validation.ValidationElapsedSeconds}");

    //SetCursorPosition(0, WindowTop + 2);
    //Write($"Elapsed (ms):  {validation.ValidationElapsedMilliseconds}");

    //SetCursorPosition(0, WindowTop + 3);
    //Write($"Elapsed (ns):  {validation.ValidationElapsedMilliseconds * 1000000}");

    //double average = 0;

    //if (validation.ValidationElapsedTicks > 0)
    //{
    //    average = validatorElapsedTimerAverager.GetAverage(validation.ValidationElapsedTicks ?? 0);
    //}


    //SetCursorPosition(0, WindowTop + 4);
    //Write($"Current AVG (MS): {average / (double)TimeSpan.TicksPerMillisecond}");
}

//SetCursorPosition(0, WindowTop + 6);
//Write($"Current AVG (MS):   {validatorElapsedTimerAverager.CurrentAvg}");

//SetCursorPosition(0, WindowTop + 8);
//Write($"Highest AVG (MS):   {validatorElapsedTimerAverager.HighestAvg}");

//SetCursorPosition(0, WindowTop + 9);
//Write($"Lowest AVG (MS):    {validatorElapsedTimerAverager.LowestAvg}");

//SetCursorPosition(0, WindowTop + 11);
//Write($"Highest Entry (MS): {validatorElapsedTimerAverager.HighestEntry} at {validatorElapsedTimerAverager.HighestEntryOccurance}");

//SetCursorPosition(0, WindowTop + 12);
//Write($"Lowest Entry (MS):  {validatorElapsedTimerAverager.LowestEntry} at {validatorElapsedTimerAverager.LowestEntryOccurance}");

//SetCursorPosition(0, WindowTop + 14);
//Write($"Total Entries:  {validatorElapsedTimerAverager.TotalEntries}");