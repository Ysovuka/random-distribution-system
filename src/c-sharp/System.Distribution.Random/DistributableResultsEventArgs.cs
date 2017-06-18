using System;
using System.Collections.Generic;
using System.Text;

namespace System.Distribution
{
    public class DistributableResultsEventArgs : EventArgs
    {
        public DistributableResultsEventArgs(IEnumerable<IDistributable> results)
        {
            Results = results;
        }
        public IEnumerable<IDistributable> Results { get; private set; } = new List<IDistributable>();
    }
}
