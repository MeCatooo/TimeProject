// See https://aka.ms/new-console-template for more information
using TimeProject;

//Console.WriteLine("Hello, World!");
Time time = new Time(1, 1, 1);
Time time1 = new Time(1, 11, 10);
TimePeriod TimePeriod = new TimePeriod(time1,time);
Console.WriteLine(TimePeriod.ToString());