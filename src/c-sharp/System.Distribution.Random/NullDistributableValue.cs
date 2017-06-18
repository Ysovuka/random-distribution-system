using System;
using System.Collections.Generic;
using System.Text;

namespace System.Distribution
{
    public class NullDistributableValue : DistributableValue<object>
    {
        public NullDistributableValue(double probability)
            : base(null, probability, false, false, true) { }
    }
}
