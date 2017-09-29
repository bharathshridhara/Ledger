using System.Collections.Generic;
using System.Web.Http.Routing;
using Ledger.Data;
using Ledger.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ledger.Helpers
{
    public interface IVMFactory
    {
        AccountViewModel CreateAccountVM(Account account, IUrlHelper urlHelper);
        ICollection<EntryViewModel> CreateEntryVM(IEnumerable<Entry> entries, IUrlHelper urlHelper);
    }
}