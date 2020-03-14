using System;
using System.Collections.Generic;
using System.Text;

namespace PersonasDTO
{
    public class CommonBase
    {
        private static Int64 _Int64_NullValue = Int64.MinValue;

        public static DateTime DateTime_NullValue { get; set; } = DateTime.MinValue;

        public static int Int_NullValue { get; set; } = int.MinValue;
        public static decimal Dec_NullValue { get; set; } = 0;
        public static float Float_NullValue { get; set; } = float.MinValue;

        public static string String_NullValue { get; set; } = string.Empty;

        public static char Char_NullValue { get; set; } = char.MinValue;
        public static Int64 Int64_NullValue
        {
            get { return _Int64_NullValue; }
            set { _Int64_NullValue = value; }
        }
    }
}
