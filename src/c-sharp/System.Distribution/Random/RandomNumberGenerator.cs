using System;
using System.Collections.Generic;
using System.Text;

namespace System.Distribution.Random
{
    public static class RandomNumberGenerator
    {
        private static IRandomNumberGenerator _randomizer;

        static RandomNumberGenerator()
        {
            _randomizer = new CryptograpbhyRandomNumberGenerator();
        }

        public static double GetValue(double maximum)
        {
            return GetValue(1.0, maximum);
        }

        public static double GetValue(double minimum, double maximum)
        {
            return _randomizer.GetValue(minimum, maximum);
        }

        public static int GetValue(int maximum)
        {
            return GetValue(1, maximum);
        }

        public static int GetValue(int minimum, int maximum)
        {
            return _randomizer.GetValue(minimum, maximum);
        }
    }
}
