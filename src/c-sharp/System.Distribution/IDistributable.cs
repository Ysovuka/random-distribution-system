using System;
using System.Collections.Generic;
using System.Text;

namespace System.Distribution
{
    public interface IDistributable
    {
        bool AlwaysDrop { get; set; }
        bool IsActive { get; set; }
        bool IsUnique { get; set; }
        double Probability { get; set; }
        IDistributableTable Table { get; set; }

        void Attach(IDistributableTable table);
        void Detach(IDistributableTable table);
    }
}
