using AutoMapper;
using Ledger.Data;
using Ledger.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ledger.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AccountViewModel, Account>().ReverseMap();
            CreateMap<EntryViewModel, Entry>().ReverseMap();
        }
    }
}
