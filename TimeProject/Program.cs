// See https://aka.ms/new-console-template for more information
using TimeProject;

//Console.WriteLine("Hello, World!");
Time time = new Time(1, 1, 1);
Time time1 = new Time(11, 1, 1);
TimePeriod TimePeriod = new TimePeriod(time,time1);
Console.WriteLine(TimePeriod.ToString());