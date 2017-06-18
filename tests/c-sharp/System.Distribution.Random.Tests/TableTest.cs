using System;
using System.Collections.Generic;
using System.Distribution.Tests.Mock;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace System.Distribution.Tests
{
    public class TableTest
    {
        private readonly ITestOutputHelper _output;
        public TableTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void EnsureOneResultFromTwoItems()
        {
            DistributableTable table = new DistributableTable();
            table.AddEntry(new Item("Item 1"), 10);
            table.AddEntry(new Item("Item 2"), 10);

            table.MaximumResultCount = 1;
            Assert.Equal(table.MaximumResultCount, table.GetResults().Count());
        }

        [Fact]
        public void EnsureTwoResultsFromThreeItems()
        {
            DistributableTable table = new DistributableTable();
            table.AddEntry(new Item("Item 1"), 10);
            table.AddEntry(new Item("Item 2"), 10);
            table.AddEntry(new Item("Item 3")
            {
                AlwaysDrop = true
            });

            table.MaximumResultCount = 2;
            Assert.Equal(table.MaximumResultCount, table.GetResults().Count());
        }

        [Fact]
        public void EnsureOnlyOneUniqueItem()
        {
            DistributableTable table = new DistributableTable();
            DistributableTable subTable1 = new DistributableTable();
            DistributableTable subTable2 = new DistributableTable()
            {
                IsUnique = true
            };
            DistributableTable subTable3 = new DistributableTable();

            table.AddEntry(subTable1, 10);
            table.AddEntry(subTable2, 10);
            table.AddEntry(subTable3, 10);

            subTable1.AddEntry(new Item("Table 1 - Item 1"), 10);
            subTable1.AddEntry(new Item("Table 1 - Item 2"), 10);
            subTable1.AddEntry(new Item("Table 1 - Item 3"), 10);
            
            subTable2.AddEntry(new Item("Table 2 - Item 1"), 10);
            subTable2.AddEntry(new Item("Table 2 - Item 2"), 10);
            subTable2.AddEntry(new Item("Table 2 - Item 3"), 10);

            subTable3.AddEntry(new Item("Table 3 - Item 1"), 10);
            subTable3.AddEntry(new Item("Table 3 - Item 2"), 10);
            subTable3.AddEntry(new Item("Table 3 - Item 3"), 10);

            table.MaximumResultCount = 10;
            IEnumerable<IDistributable> results = table.GetResults();

            foreach(var entry in results)
            {
                _output.WriteLine($"{(entry as Item).Name}");
            }

            Assert.Equal(table.MaximumResultCount, results.Count());
            Assert.True(results.Count(e => e.IsUnique) <= 1);
        }

        [Fact]
        public void EnsureItemsSetToAlwaysDropWillAlwaysDrop()
        {
            DistributableTable table = new DistributableTable();
            DistributableTable subTable1 = new DistributableTable();
            DistributableTable subTable2 = new DistributableTable()
            {
                AlwaysDrop = true,
                IsUnique = true
            };
            DistributableTable subTable3 = new DistributableTable();

            table.AddEntry(subTable1, 10);
            table.AddEntry(subTable2, 10);
            table.AddEntry(subTable3, 10);

            subTable1.AddEntry(new Item("Table 1 - Item 1"), 10);
            subTable1.AddEntry(new Item("Table 1 - Item 2"), 10);
            subTable1.AddEntry(new Item("Table 1 - Item 3"), 10);

            subTable2.AddEntry(new Item("Table 2 - Item 1"), 10);
            subTable2.AddEntry(new Item("Table 2 - Item 2"), 10);
            subTable2.AddEntry(new Item("Table 2 - Item 3"), 10);

            subTable3.AddEntry(new Item("Table 3 - Item 1"), 10);
            subTable3.AddEntry(new Item("Table 3 - Item 2"), 10);
            subTable3.AddEntry(new Item("Table 3 - Item 3"), 10);

            table.MaximumResultCount = 10;
            IEnumerable<IDistributable> results = table.GetResults();

            foreach (var entry in results)
            {
                _output.WriteLine($"{(entry as Item).Name}");
            }

            Assert.Equal(table.MaximumResultCount, results.Count());
            Assert.Equal(1, results.Count(e => (e as Item).Name.StartsWith("Table 2")));
        }
    }
}
