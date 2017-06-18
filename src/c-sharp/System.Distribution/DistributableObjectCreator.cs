using System;
using System.Collections.Generic;
using System.Text;

namespace System.Distribution
{
    public class DistributableObjectCreator : DistributableObject, IDistributableCreator
    {
        public virtual IDistributable CreateInstance()
        {
            return (IDistributable)Activator.CreateInstance(this.GetType());
        }
    }
}
