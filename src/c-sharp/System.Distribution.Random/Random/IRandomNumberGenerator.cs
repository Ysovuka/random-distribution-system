using System;
using System.Collections.Generic;
using System.Text;

namespace System.Distribution.Random
{
    public interface IRandomNumberGenerator
    {
        double GetValue(double maximum);
        double GetValue(double minimum, double maximum);
        int GetValue(int maximum);
        int GetValue(int minimum, int maximum);
    }
}
