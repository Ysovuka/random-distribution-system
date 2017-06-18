using System;
using System.Collections.Generic;
using System.Text;

namespace System.Distribution.Tests.Mock
{
    public class Item : DistributableObject
    {
        public Item(string name) : base()
        {
            Name = name;
        }
        public string Name { get; set; }

    }
}
