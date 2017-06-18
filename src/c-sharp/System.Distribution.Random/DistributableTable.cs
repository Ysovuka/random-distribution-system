using System;
using System.Collections.Generic;
using System.Distribution.Random;
using System.Linq;
using System.Text;

namespace System.Distribution
{
    public class DistributableTable : IDistributableTable
    {
        public DistributableTable()
            : this(null, 1, 1, false, false, true) { }
        public DistributableTable(IEnumerable<IDistributable> contents, int maximumResultsCount, double probability)
            : this(contents, maximumResultsCount, probability, false, false, true) { }
        public DistributableTable(IEnumerable<IDistributable> contents, int maximumResultsCount, double probability, bool unique, bool always, bool active)
        {
            if (contents != null)
                Contents = contents.ToList();

            MaximumResultCount = maximumResultsCount;
            Probability = probability;
            AlwaysDrop = always;
            IsUnique = unique;
            IsActive = active;
        }

        public event EventHandler BeforeEvaluation;
        public event EventHandler<DistributableResultsEventArgs> AfterEvaluation;

        public List<IDistributable> Contents { get; private set; } = new List<IDistributable>();
        public int MaximumResultCount { get; set; }

        public bool AlwaysDrop { get ; set ; }
        public bool IsActive { get; set; }
        public bool IsUnique { get; set; }
        public double Probability { get; set; }
        public IDistributableTable Table { get; set; }

        public virtual void AddEntry(IDistributable entry)
        {
            Contents.Add(entry);
            entry.Attach(this);
        }        
        public virtual void AddEntry(IDistributable entry, double probability)
        {
            AddEntry(entry);
            entry.Probability = probability;
        }
        public virtual void AddEntry(IDistributable entry, double probability, bool unique, bool always, bool active)
        {
            AddEntry(entry, probability);
            entry.AlwaysDrop = always;
            entry.IsUnique = unique;
            entry.IsActive = active;
        }

        public virtual void ClearContents()
        {
            Contents.Clear();
        }

        public virtual void RemoveEntry(IDistributable entry)
        {
            Contents.Remove(entry);
            entry.Detach(this);
        }
        public virtual void RemoveEntry(int index)
        {
            RemoveEntry(Contents[index]);
        }

        public virtual IEnumerable<IDistributable> GetResults()
        {
            List<IDistributable> results = new List<IDistributable>();

            BeforeEvaluation?.Invoke(this, EventArgs.Empty);

            PopulateResultsWithItemsSetToAlwaysDrop(results);

            PopulateResultsWithItemsNotSetToAlwaysDrop(results);

            AfterEvaluation?.Invoke(this, new DistributableResultsEventArgs(results));

            return results;
        }

        private bool AddItemToResults(List<IDistributable> results, IDistributable entry)
        {
            if (entry.IsUnique && results.Contains(entry))
                return false;

            if (!(entry is NullDistributableValue))
            {
                if (entry is DistributableTable)
                {
                    results.AddRange(((IDistributableTable)entry).GetResults());
                }
                else
                {
                    IDistributable entity = entry;
                    if (entry is IDistributableCreator)
                    {
                        entity = ((IDistributableCreator)entry).CreateInstance();
                    }

                    results.Add(entity);
                }
            }

            return true;
        }

        private void PopulateResultsWithItemsSetToAlwaysDrop(List<IDistributable> results)
        {
            foreach(IDistributable entry in Contents.Where(e => e.AlwaysDrop && e.IsActive))
            {
                AddItemToResults(results, entry);
            }
        }

        private void PopulateResultsWithItemsNotSetToAlwaysDrop(List<IDistributable> results)
        {
            int remainingDropCount = CalculateRemainingItemDropCount();

            if (remainingDropCount > 0)
            {
                for (int dropCount = 0; dropCount < remainingDropCount; dropCount++)
                {
                    IEnumerable<IDistributable> distributableItems = Contents.Where(e => !e.AlwaysDrop && e.IsActive);
                    double magicProbabilityNumber = RandomNumberGenerator.GetValue(distributableItems.Sum(e => e.Probability));

                    double runningValue = 0;
                    foreach(IDistributable entry in distributableItems)
                    {
                        runningValue += entry.Probability;
                        if (magicProbabilityNumber <= runningValue)
                        {
                            if (AddItemToResults(results, entry))
                                break;
                        }
                    }
                }
            }
        }

        private int CalculateRemainingItemDropCount()
            => MaximumResultCount - Contents.Count(e => e.AlwaysDrop && e.IsActive);

        public void Attach(IDistributableTable table)
        {
            Table = table;
            table.BeforeEvaluation += OnBeforeEvaluation;
            table.AfterEvaluation += OnAfterEvaluation;
        }

        public void Detach(IDistributableTable table)
        {
            if (Table == table)
            {
                Table.BeforeEvaluation -= OnBeforeEvaluation;
                Table.AfterEvaluation -= OnAfterEvaluation;
                Table = null;
            }
        }

        protected virtual void OnAfterEvaluation(object sender, DistributableResultsEventArgs e)
        {
            // Blank to allow objects to override here.
        }

        protected virtual void OnBeforeEvaluation(object sender, EventArgs e)
        {
            // Blank to allow objects to override here.
        }
    }
}
