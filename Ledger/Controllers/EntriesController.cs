using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ledger.Data;
using Ledger.ViewModels;
using AutoMapper;
using Ledger.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ledger.Controllers
{
    [Route("api/accounts/accountid/entries")]
    public class EntriesController : Controller
    {
        private ILedgerRepository _repo;
        private IMapper _mapper;
        private IVMFactory _vmFactory;

        public EntriesController(ILedgerRepository repo, IMapper mapper, IVMFactory vmFactory)
        {
            _repo = repo;
            _mapper = mapper;
            _vmFactory = vmFactory;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get(long accountid)
        {
            var entries = _repo.GetEntriesForAccount(accountid);
            if(entries != null)
            {
                var entriesVM = _vmFactory.CreateEntryVM(entries.ToList(), Url);
                return Ok(entriesVM);
            }
            return NotFound();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(long accountid, long id)
        {
            var model = _repo.GetEntryById(accountid, id);
            if (model != null)
            {
                var modelToList = new List<Entry>();
                modelToList.Add(model);
                var entryVM = _vmFactory.CreateEntryVM(modelToList, Url).FirstOrDefault();
                return Ok(model);
            }
            else
                return NotFound();
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post(long accountid, [FromBody]EntryViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var entry = _mapper.Map<Entry>(viewModel);
                var result = await _repo.AddEntryAsync(accountid, entry);
                if (result > 0)
                    return Created("", viewModel);
                else
                    return BadRequest(ModelState);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long accountid, long id, [FromBody]EntryViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var entry = _mapper.Map<Entry>(viewModel);
                var result = await _repo.SaveEntryAsync(accountid, id, entry);
                if (result > 0)
                    return Created("", viewModel);
                else
                    return NotFound();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        
        public async Task<IActionResult> Delete(long accountid, long id)
        {
            var result = await _repo.DeleteEntryFromAccountAsync(accountid, id);
            if (result > 0)
            {
                return Ok();
            }
            else
                return NotFound();
        }
    }
}
