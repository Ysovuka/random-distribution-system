using System;
using System.Collections.Generic;
using System.Text;

namespace System.Distribution
{
    public interface IDistributableTable : IDistributable
    {
        event EventHandler BeforeEvaluation;
        event EventHandler<DistributableResultsEventArgs> AfterEvaluation;

        List<IDistributable> Contents { get; }
        int MaximumResultCount { get; set; }

        IEnumerable<IDistributable> GetResults();
    }
}
