using AutoMapper;
using Ledger.Data;
using Ledger.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Routing;

namespace Ledger.Helpers
{
    public class VMFactory : IVMFactory
    {
        private IMapper _mapper;

        public VMFactory(IMapper mapper)
        {
            _mapper = mapper;
        }
        public AccountViewModel CreateAccountVM(Account account, IUrlHelper urlHelper)
        {
            var viewModel = _mapper.Map<AccountViewModel>(account);
            viewModel.Url = urlHelper.Link("accounts", new { id = account.Id });
            viewModel.EntriesUrl = urlHelper.Link("entries", new { accountid = account.Id });
            viewModel.Entries = CreateEntryVM(account.Entries, urlHelper);
            
            return viewModel;
        }

        public ICollection<EntryViewModel> CreateEntryVM(IEnumerable<Entry> entries, IUrlHelper urlHelper)
        {
            ICollection<EntryViewModel> viewModels = new List<EntryViewModel>();
            foreach (Entry entry in entries)
            {
                var viewModel = _mapper.Map<EntryViewModel>(entry);
                viewModel.Url = urlHelper.Link("entries", new { accountid = entry.AccountId, id = entry.Id });
                viewModels.Add(viewModel);
            }
            return viewModels;
        }
    }
}
