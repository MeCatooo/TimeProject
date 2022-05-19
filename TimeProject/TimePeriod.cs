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
            if(hours < 0)
                throw new ArgumentOutOfRangeException();
            _seconds = hours * 3600;
        }
        public TimePeriod(int hours, int minutes)
        {
            if (hours < 0 || minutes < 0)
                throw new ArgumentOutOfRangeException();
            _seconds = hours * 3600 + minutes * 60;
        }
        public TimePeriod(int hours, int minutes, int seconds)
        {
            if (hours < 0 || minutes < 0 || seconds<0)
                throw new ArgumentOutOfRangeException();
            _seconds = hours * 3600 + minutes * 60 + seconds;
        }
        public TimePeriod(string time)
        {
            int[] dane = Array.ConvertAll<string, int>(time.Split(":"), int.Parse);
            int sec = 0;
            if (dane[0] < 0 || dane[1] < 0 || dane[2] < 0)
                throw new ArgumentOutOfRangeException();
            foreach (var item in dane)
            {
                Console.WriteLine(item);
            }
            sec += dane[0] * 3600;
            sec += dane[1] * 60;
            sec += dane[2];
            _seconds = sec;

        }
        public TimePeriod(Time time1, Time time2)
        {
            if (time1.Equals (time2))
                _seconds = 0;
            else if (time1 > time2)
                throw new ArgumentException();
            else
            {
                long time1Seconds = time1.Hours * 3600 + time1.Minutes * 60 + time1.Seconds;
                long time2Seconds = time2.Hours * 3600 + time2.Minutes * 60 + time2.Seconds;
                _seconds = Math.Abs(time2Seconds - time1Seconds);
            }
        }
        /// <summary>
        /// Zwraca ciąg znaków przedstawiający okres czasu.
        /// </summary>
        /// <returns>String w postaci HH:MM:SS</returns>
        public override string ToString()
        {
            if (_seconds == 0)
                return "00:00:00";
            double localHours = Math.Floor((double)(_seconds / 3600));
            double localMinutes = Math.Floor((_seconds - (localHours * 3600)) / 60);
            double localSeconds = _seconds - (localHours * 3600) - (localMinutes * 60);
            return $"{localHours:00}:{localMinutes:00}:{localSeconds:00}";
        }
        /// <summary>
        /// Sprawdza równość czasu trwania dwóch TimePeriod.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>bool</returns>
        public bool Equals(TimePeriod other)
        {
            return _seconds == other._seconds;
        }
        /// <summary>
        /// Porównuje czas trwania dwóch TimePeriod.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>int</returns>
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
        /// <summary>
        /// Dodaje dwa TimePeriod.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>TimePeriod</returns>
        public TimePeriod Plus(TimePeriod other)
        {
            long Seconds = _seconds + other._seconds;
            double localHours = Math.Floor((double)(Seconds / 3600));
            double localMinutes = Math.Floor((Seconds - (localHours * 3600)) / 60);
            double localSeconds = Seconds - (localHours * 3600) - (localMinutes * 60);
            return new TimePeriod((int)localHours, (int)localMinutes, (int)localSeconds);
        }
        /// <summary>
        /// Dodaje dwa TimePeriod.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>TimePeriod</returns>
        public static TimePeriod Plus(TimePeriod a, TimePeriod b)
        {
            long Seconds = a._seconds + b._seconds;
            double localHours = Math.Floor((double)(Seconds / 3600));
            double localMinutes = Math.Floor((Seconds - (localHours * 3600)) / 60);
            double localSeconds = Seconds - (localHours * 3600) - (localMinutes * 60);
            return new TimePeriod((int)localHours, (int)localMinutes, (int)localSeconds);
        }
        public static TimePeriod operator +(TimePeriod a, TimePeriod b)
        {
            return a.Plus(b);
        }
        /// <summary>
        /// Odejmuje dwa TimePeriod.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>TimePeriod</returns>
        public TimePeriod Substract(TimePeriod other)
        {
            long Seconds = _seconds - other._seconds;
            double localHours = Math.Floor((double)(Seconds / 3600));
            double localMinutes = Math.Floor((Seconds - (localHours * 3600)) / 60);
            double localSeconds = Seconds - (localHours * 3600) - (localMinutes * 60);
            return new TimePeriod((int)localHours, (int)localMinutes, (int)localSeconds);
        }
        /// <summary>
        /// Odejmuje dwa TimePeriod.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>TimePeriod</returns>
        public static TimePeriod Substract(TimePeriod a, TimePeriod b)
        {
            long Seconds = a._seconds - b._seconds;
            double localHours = Math.Floor((double)(Seconds / 3600));
            double localMinutes = Math.Floor((Seconds - (localHours * 3600)) / 60);
            double localSeconds = Seconds - (localHours * 3600) - (localMinutes * 60);
            return new TimePeriod((int)localHours, (int)localMinutes, (int)localSeconds);
        }
        public static TimePeriod operator -(TimePeriod a, TimePeriod b)
        {
            return a.Substract(b);
        }
        /// <summary>
        /// Sprawdza równość czasu trwania dwóch TimePeriod. Zwraca false dla innych typów.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>bool</returns>
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
