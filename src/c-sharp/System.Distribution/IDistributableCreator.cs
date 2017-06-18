using System;
using System.Collections.Generic;
using System.Text;

namespace System.Distribution
{
    public interface IDistributableCreator : IDistributable
    {
        IDistributable CreateInstance();
    }
}
