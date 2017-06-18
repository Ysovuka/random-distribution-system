using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace System.Distribution.Random
{
    public class CryptograpbhyRandomNumberGenerator : IRandomNumberGenerator
    {
        private Security.Cryptography.RandomNumberGenerator random =
            Security.Cryptography.RandomNumberGenerator.Create();

        private byte[] mbuffer = new byte[8];

        public double GetValue(double maximum)
        {
            return GetValue(1.0, maximum);
        }

        public double GetValue(double minimum, double maximum)
        {
            random.GetBytes(mbuffer);
            UInt64 rand = BitConverter.ToUInt64(mbuffer, 0);
            double results = rand / (minimum + UInt64.MaxValue);
            return Math.Ceiling(results * maximum);
        }

        public int GetValue(int maximum)
        {
            return GetValue(1, maximum);
        }

        public int GetValue(int minimum, int maximum)
        {
            return Convert.ToInt32(GetValue(minimum, (double)maximum));
        }
    }
}
