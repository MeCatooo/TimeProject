using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeProject
{
    public struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        private readonly ulong _seconds = 0;

        public TimePeriod(uint hours)
        {
            _seconds = hours * 3600;
        }
        public TimePeriod(uint hours, uint minutes)
        {
            _seconds = hours * 3600 + minutes * 60;
        }
        public TimePeriod(uint hours, uint minutes, uint seconds)
        {
            _seconds = hours * 3600 + minutes * 60 + seconds;
        }
        public TimePeriod(string time)
        {
            uint[] dane = Array.ConvertAll<string, uint>(time.Split(":"), uint.Parse);
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
                _seconds = (ulong)Math.Abs(time2Seconds - time1Seconds);
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
        public override int GetHashCode()
        {
            return _seconds.GetHashCode();
        }
        public static bool operator ==(TimePeriod a, TimePeriod b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TimePeriod a, TimePeriod b)
        {
            return !a.Equals(b);
        }
        public static bool operator <(TimePeriod a, TimePeriod b)
        {
            return a.CompareTo(b) < 0;
        }
        public static bool operator >(TimePeriod a, TimePeriod b)
        {
            return a.CompareTo(b) > 0;
        }
        public static bool operator <=(TimePeriod a, TimePeriod b)
        {
            return a.CompareTo(b) <= 0;
        }
        public static bool operator >=(TimePeriod a, TimePeriod b)
        {
            return a.CompareTo(b) >= 0;
        }
        public TimePeriod Plus(TimePeriod other)
        {
            ulong Seconds = _seconds + other._seconds;
            double localHours = Math.Floor((double)(Seconds / 3600));
            double localMinutes = Math.Floor((Seconds - (localHours * 3600)) / 60);
            double localSeconds = Seconds - (localHours * 3600) - (localMinutes * 60);
            return new TimePeriod((uint)localHours, (uint)localMinutes, (uint)localSeconds);
        }
        public static TimePeriod Plus(TimePeriod a, TimePeriod b)
        {
            ulong Seconds = a._seconds + b._seconds;
            double localHours = Math.Floor((double)(Seconds / 3600));
            double localMinutes = Math.Floor((Seconds - (localHours * 3600)) / 60);
            double localSeconds = Seconds - (localHours * 3600) - (localMinutes * 60);
            return new TimePeriod((uint)localHours, (uint)localMinutes, (uint)localSeconds);
        }
        public static TimePeriod operator +(TimePeriod a, TimePeriod b)
        {
            return a.Plus(b);
        }

        public TimePeriod Substract(TimePeriod other)
        {
            ulong Seconds = _seconds - other._seconds;
            double localHours = Math.Floor((double)(Seconds / 3600));
            double localMinutes = Math.Floor((Seconds - (localHours * 3600)) / 60);
            double localSeconds = Seconds - (localHours * 3600) - (localMinutes * 60);
            return new TimePeriod((uint)localHours, (uint)localMinutes, (uint)localSeconds);
        }
        public static TimePeriod Substract(TimePeriod a, TimePeriod b)
        {
            ulong Seconds = a._seconds - b._seconds;
            double localHours = Math.Floor((double)(Seconds / 3600));
            double localMinutes = Math.Floor((Seconds - (localHours * 3600)) / 60);
            double localSeconds = Seconds - (localHours * 3600) - (localMinutes * 60);
            return new TimePeriod((uint)localHours, (uint)localMinutes, (uint)localSeconds);
        }
        public static TimePeriod operator -(TimePeriod a, TimePeriod b)
        {
            return a.Substract(b);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;
            if (obj is not Time)
                return false;
            return Equals((TimePeriod)obj);
        }
    }
}
