using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ledger.ViewModels
{
    public class AccountViewModel
    {
        public long Id { get; set; }
        public string Url { get; set; }
        public string EntriesUrl { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }
        public ICollection<EntryViewModel> Entries { get; set; }
    }
}
