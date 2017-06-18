using System;
using System.Collections.Generic;
using System.Text;

namespace System.Distribution
{
    public interface IDistributableValue<T> : IDistributable
    {
        T Value { get; set; }
    }
}
