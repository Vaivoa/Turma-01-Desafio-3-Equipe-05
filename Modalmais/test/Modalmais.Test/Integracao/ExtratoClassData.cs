using System;
using System.Collections;
using System.Collections.Generic;

namespace Modalmais.Test
{
    internal class ExtratoClassData : IEnumerable<object[]>
    {
        public static int year { get; set; }
        public static int month { get; set; }
        public static int day { get; set; }

        private readonly List<object[]> _data = new()
        {
            new object[] { null, DateTime.Now, true },
            new object[] { DateTime.Now, null, true },
            new object[] { null, null, true },
            new object[] { new DateTime(), new DateTime(), true },
            new object[] { DateTime.Now, DateTime.Now.AddDays(1), false },
            new object[] { DateTime.Now.AddDays(1), DateTime.Now.AddDays(1), false },
            new object[] { DateTime.Now, DateTime.Now, false },
        };

        private static DateTime DataInvalida()
        {
            return new DateTime(year: DateTime.Now.Month <= 5 ? DateTime.Now.Year - 1 : DateTime.Now.Year,
                    month: DateTime.Now.Month > 5 ? DateTime.Now.Month - 5 : DateTime.Now.Month,
                    day: DateTime.Now.Day);
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}