using System;

namespace Ledger.ViewModels
{
    public class EntryViewModel
    {
        public string Url { get; set; }
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public bool IsDeposit { get; set; }
    }
}