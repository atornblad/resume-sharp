using System;

namespace ResumeSharp
{
    public class Compare
    {
        private int result;

        private Compare()
        {
        }

        public static Compare These<T>(T left, T right) where T : IComparable<T>
        {
            return new Compare { result = left.CompareTo(right) };
        }

        public Compare Then<T>(T left, T right) where T : IComparable<T>
        {
            if (result != 0) return this;
            result = left.CompareTo(right);
            return this;
        }

        public static implicit operator int(Compare compare)
        {
            return compare.result;
        }
    }
}
