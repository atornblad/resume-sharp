using System;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using ResumeSharp.Formatting;

namespace ResumeSharp.Schema
{
    [JsonConverter(typeof(Iso8601Converter))]
    public class Date : IEquatable<Date>, IComparable<Date>, ICloneable
    {
        public int Year { get; init; }
        private int? Month { get; init; }
        private int? DayOfMonth { get; init; }

        private Date()
        {
        }

        public Date(int year)
        {
            ValidateYear(year);
            Year = year;
        }

        public Date(int year, int month) : this(year)
        {
            ValidateMonth(month);
            Month = month;
        }
        
        public Date(int year, int month, int day) : this(year, month)
        {
            ValidateDayOfMonth(year, month, day);
            DayOfMonth = day;
        }

        public Date(DateTime dateTime) : this(dateTime.Year, dateTime.Month, dateTime.Day)
        {
        }

        public Date(DateTimeOffset dateTimeOffset) : this(dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day)
        {
        }

        public Date(string value)
        {
            int[] parts;

            try
            {
                parts = value.Split('-').Select(part => part.Trim().TrimStart('0')).Select(part => int.Parse(part)).ToArray();
            }
            catch
            {
                throw new ArgumentException("Not a valid ISO8601 date format", nameof(value));
            }

            int year = parts[0];
            ValidateYear(year);
            Year = year;

            if (parts.Length < 2) return;
            int month = parts[1];
            ValidateMonth(month);
            Month = month;

            if (parts.Length < 3) return;
            int dayOfMonth = parts[2];
            ValidateDayOfMonth(year, month, dayOfMonth);
            DayOfMonth = dayOfMonth;

            if (parts.Length > 3)
            {
                throw new ArgumentException("Not a valid ISO8601 date format", nameof(value));
            }
        }

        private void ValidateYear(int year)
        {
            if (year < 1000 || year > 2999)
            {
                throw new ArgumentOutOfRangeException(nameof(year), year, "The value of year must be between 1000 and 2999 inclusive.");
            }
        }

        private void ValidateMonth(int month)
        {
            if (month < 1 || month > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(month), month, "The value of month must be between 1 and 12 inclusive.");
            }
        }

        private void ValidateDayOfMonth(int year, int month, int day)
        {
            int daysInMonth = DateTime.DaysInMonth(year, month);
            if (day < 1 || day > daysInMonth)
            {
                throw new ArgumentOutOfRangeException(nameof(day), day, $"The value of day must be between 1 and {daysInMonth} inclusive.");
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(Year);
            
            if (Month.HasValue)
            {
                builder.Append('-');
                builder.Append(Month.Value.ToString("00"));

                if (DayOfMonth.HasValue)
                {
                    builder.Append('-');
                    builder.Append(DayOfMonth.Value.ToString("00"));
                }
            }

            return builder.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is Date date)
            {
                return Equals(date);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (Year * 10000 + (Month ?? 0) * 100 + (DayOfMonth ?? 0)) ^ 0x2a9c1f3;
        }

        public bool Equals(Date other)
        {
            if (other == null) return false;
            return other.Year == Year && other.Month == Month && other.DayOfMonth == DayOfMonth;
        }

        public int CompareTo(Date other)
        {
            return Compare.These(Year, other.Year).Then(Month ?? 0, other.Month ?? 0).Then(DayOfMonth ?? 0, other.DayOfMonth ?? 0);
        }

        public object Clone()
        {
            return new Date { Year = Year, Month = Month, DayOfMonth = DayOfMonth };
        }

        public DateTime AsDateTime()
        {
            return new DateTime(Year, Month ?? 1, DayOfMonth ?? 1);
        }

        public DateTimeOffset AsDateTimeOffset()
        {
            return new DateTimeOffset(AsDateTime());
        }
    }
}
