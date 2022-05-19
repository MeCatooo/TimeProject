using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeProject
{
    public struct Time : IEquatable<Time>, IComparable<Time>
    {
        private readonly byte hours = 0;
        private readonly byte minutes = 0;
        private readonly byte seconds = 0;
        /// <summary>
        /// Zwraca reprezentację godzin.
        /// </summary>
        public byte Hours
        {
            get { return hours; }
            init
            {
                if (value < 0 || value >= 24)
                    throw new ArgumentOutOfRangeException();
                hours = value;
            }
        }
        /// <summary>
        /// Zwraca reprezentację minut.
        /// </summary>
        public byte Minutes
        {
            get { return minutes; }
            init
            {
                if (value < 0 || value >= 60)
                    throw new ArgumentOutOfRangeException();
                minutes = value;
            }
        }
        /// <summary>
        /// Zwraca reprezentację sekund.
        /// </summary>
        public byte Seconds
        {
            get { return seconds; }
            init
            {
                if (value < 0 || value >= 60)
                    throw new ArgumentOutOfRangeException();
                seconds = value;
            }
        }

        public Time(byte hours, byte minutes, byte seconds)
        {
            Hours = hours; 
            Minutes = minutes;
            Seconds = seconds;
        }
        public Time(byte hours, byte minutes)
        {
            Hours = hours;
            Minutes = minutes;
        }
        public Time(byte hours)
        {
            Hours = hours;
        }
        public Time(string time)
        {
            byte[] dane = Array.ConvertAll<string, byte>(time.Split(":"), byte.Parse);
            Hours = dane[0];
            Minutes = dane[1];
            Seconds = dane[2];
        }
        /// <summary>
        /// Zwraca ciąg znaków przedstawiający punkt w czasie.
        /// </summary>
        /// <returns>String w postaci HH:MM:SS</returns>
        public override string ToString()
        {
            return $"{hours:00}:{minutes:00}:{seconds:00}";
        }
        /// <summary>
        /// Sprawdza równość wskazywanego punktu w czasie dla dwóch Time.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>bool</returns>
        public bool Equals(Time other)
        {
            if (other.Hours == Hours && other.Minutes == Minutes && other.Seconds == Seconds)
                return true;
            return false;
        }
        /// <summary>
        /// Sprawdza równość wskazywanego punktu w czasie dla dwóch Time. Zwraca false dla innych typów.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>bool</returns>
        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj == null)
                return false;
            if (obj is not Time)
                return false;
            return Equals((Time)obj);
        }
        /// <summary>
        /// Porównuje wskazywany punktu w czasie dla dwóch Time.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>bool</returns>
        public int CompareTo(Time other)
        {
            int localSum = Hours * 3600 + Minutes * 60 + Seconds;
            int otherSum = other.Hours * 3600 + other.Minutes * 60 + other.Seconds;
            return localSum - otherSum;
        }
        public override int GetHashCode()
        {
            return Hours * 3600 + Minutes * 60 + Seconds;
        }
        public static bool operator ==(Time a, Time b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Time a, Time b)
        {
            return !a.Equals(b);
        }
        public static bool operator <(Time a, Time b)
        {
            return a.CompareTo(b) < 0;
        }
        public static bool operator >(Time a, Time b)
        {
            return a.CompareTo(b) > 0;
        }
        public static bool operator <=(Time a, Time b)
        {
            return a.CompareTo(b) <= 0;
        }
        public static bool operator >=(Time a, Time b)
        {
            return a.CompareTo(b) >= 0;
        }
        /// <summary>
        /// Dodaje podany TimePeriod do podanego Time.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>Time</returns>
        public static Time TimePlus(Time a, TimePeriod b)
        {
            int[] dane = Array.ConvertAll<string, int>(b.ToString().Split(":"), int.Parse);
            int localHours = a.Hours + dane[0];
            int localMinutes = a.Minutes + dane[1];
            int localSeconds = a.Seconds + dane[2];
            if (localSeconds % 60 != 0)
            {
                int add = (localSeconds - localSeconds % 60) / 60;
                localSeconds %= 60;
                localMinutes += add;
            }
            if (localMinutes % 60 != 0)
            {
                int add = (localMinutes - localMinutes % 60) / 60;
                localMinutes %= 60;
                localHours += add;
            }
            if (localHours % 24 != 0)
                localHours %= 24;
            return new Time((byte)localHours, (byte)localMinutes, (byte)localSeconds);
        }
        /// <summary>
        /// Dodaje podany TimePeriod do obecnego Time.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>Time</returns>
        public Time TimePlus(TimePeriod b)
        {
            int[] dane = Array.ConvertAll<string, int>(b.ToString().Split(":"), int.Parse);
            int localHours = Hours + dane[0];
            int localMinutes = Minutes + dane[1];
            int localSeconds = Seconds + dane[2];
            if (localSeconds % 60 != 0)
            {
                int add = (localSeconds - localSeconds % 60) / 60;
                localSeconds %= 60;
                localMinutes += add;
            }
            if (localMinutes % 60 != 0)
            {
                int add = (localMinutes - localMinutes % 60) / 60;
                localMinutes %= 60;
                localHours += add;
            }
            if (localHours % 24 != 0)
                localHours %= 24;
            return new Time((byte)localHours, (byte)localMinutes, (byte)localSeconds);
        }
        public static Time operator +(Time a, TimePeriod b)
        {
            return a.TimePlus(b);
        }
        /// <summary>
        /// Odejmuje podany TimePeriod do podanego Time.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>Time</returns>
        public static Time TimeSubstract(Time a, TimePeriod b)
        {
            int[] dane = Array.ConvertAll<string, int>(b.ToString().Split(":"), int.Parse);
            int localHours = a.Hours - dane[0];
            int localMinutes = a.Minutes - dane[1];
            int localSeconds = a.Seconds - dane[2];
            if (localSeconds < 0)
            {
                localSeconds = 60 + localSeconds;
                localMinutes--;
            }
            if (localMinutes < 0)
            {
                localMinutes = 60 + localMinutes;
                localHours--;
            }
            if (localHours < 0)
            {
                localHours = 24 + localHours;
            }
            return new Time((byte)localHours, (byte)localMinutes, (byte)localSeconds);
        }
        /// <summary>
        /// Odejmuje podany TimePeriod do obecnego Time.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>Time</returns>
        public Time TimeSubstract(TimePeriod b)
        {
            int[] dane = Array.ConvertAll<string, int>(b.ToString().Split(":"), int.Parse);
            int localHours = Hours - dane[0];
            int localMinutes = Minutes - dane[1];
            int localSeconds = Seconds - dane[2];
            if (localSeconds < 0)
            {
                localSeconds = 60 + localSeconds;
                localMinutes--;
            }
            if (localMinutes < 0)
            {
                localMinutes = 60 + localMinutes;
                localHours--;
            }
            if (localHours < 0)
            {
                localHours = 24 + localHours;
            }
            return new Time((byte)localHours, (byte)localMinutes, (byte)localSeconds);
        }
        public static Time operator -(Time a, TimePeriod b)
        {
            return a.TimeSubstract(b);
        }

    }
}
