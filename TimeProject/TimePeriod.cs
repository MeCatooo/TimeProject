using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeProject
{
    public struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        private readonly long _seconds = 0;

        public TimePeriod(int hours)
        {
            _seconds = hours * 3600;
        }
        public TimePeriod(int hours, int minutes)
        {
            _seconds = hours * 3600 + minutes * 60;
        }
        public TimePeriod(int hours, int minutes, int seconds)
        {
            _seconds = hours * 3600 + minutes * 60 + seconds;
        }
        public TimePeriod(string time)
        {
            byte[] dane = Array.ConvertAll<string, byte>(time.Split(":"), byte.Parse);
            _seconds = +dane[0] * 3600;
            _seconds = +dane[1] * 60;
            _seconds = +dane[2];
        }
        public TimePeriod(Time time1, Time time2)
        {
            if (time1 == time2)
                _seconds = 0;
            else if (time1 < time2)
                throw new ArgumentException();
            else
            {
                long time1Seconds = time1.Hours * 3600 + time1.Minutes * 60 + time1.Seconds;
                long time2Seconds = time2.Hours * 3600 + time2.Minutes * 60 + time2.Seconds;
                _seconds = Math.Abs(time2Seconds - time1Seconds);
            }
        }
        public override string ToString()
        {
            if (_seconds == 0)
                return "00:00:00";
            double localHours = Math.Floor((double)(_seconds / 3600));
            double localMinutes = Math.Floor((_seconds - (localHours * 3600)) / 60);
            double localSeconds = _seconds - (localHours * 3600) - (localMinutes * 60);
            return $"{localHours}:{localMinutes}:{localSeconds}";
        }

        public bool Equals(TimePeriod other)
        {
            return _seconds == other._seconds;
        }

        public int CompareTo(TimePeriod other)
        {
            return (int)(_seconds - other._seconds);
        }
    }
}
