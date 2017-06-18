using System;
using System.Collections.Generic;
using System.Text;

namespace System.Distribution
{
    public class DistributableObject : IDistributable
    {
        public DistributableObject() : this(0) { }
        public DistributableObject(double probability) : this(probability, false, false, true) { }
        public DistributableObject(double probability, bool unique, bool always, bool active)
        {
            Probability = probability;
            IsUnique = unique;
            AlwaysDrop = always;
            IsActive = active;
        }

        public bool AlwaysDrop { get; set; }
        public bool IsActive { get; set; }
        public bool IsUnique { get; set; }
        public double Probability { get; set; }
        public IDistributableTable Table { get; set; }
        
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
