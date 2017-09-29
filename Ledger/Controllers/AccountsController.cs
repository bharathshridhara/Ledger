using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ledger.ViewModels;
using Ledger.Data;
using AutoMapper;
using Ledger.Helpers;
using System.Web.Http.Routing;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace Ledger.Controllers
{
    [Route("api/accounts")]
    public class AccountsController : Controller
    {
        private ILedgerRepository _repo;
        private IMapper _mapper;
        private IVMFactory _vmFactory;

        public AccountsController(ILedgerRepository repo, IMapper mapper, IVMFactory vmFactory)
        {
            _repo = repo;
            _mapper = mapper;
            _vmFactory = vmFactory;
        }
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var accounts = _repo.GetAccountsByUser(User.Identity.Name);
            ICollection<AccountViewModel> accountsVM = new List<AccountViewModel>();
            _vmFactory = new VMFactory(_mapper);
            foreach(Account account in accounts)
            {
                var accountVM = _vmFactory.CreateAccountVM(account, Url);
            }
            
            return Ok(accountsVM);   
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var account = _repo.GetAccountById(id);
            if (account != null)
            {
                var accountVM = _vmFactory.CreateAccountVM(account, Url);
                return Ok(accountVM);
            }
            else
                return NotFound();
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AccountViewModel account)
        {
            if (ModelState.IsValid)
            {
                var accountModel = _mapper.Map<Account>(account);
                var result = await _repo.AddAccountToUserAsync(accountModel, User.Identity.Name);
                if(result > 0)
                    return Created("", null);
                else
                {
                    return BadRequest("Save failed");
                }
            }
            else
            {
                return BadRequest(account);
            }
                
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]AccountViewModel account)
        {
            if(ModelState.IsValid)
            {
                var result = await _repo.UpdateAccountAsync(id, Mapper.Map<Account>(account));
                if(result > 0)
                {
                    return Ok(account);
                }
                else { return BadRequest(error: "Save failed"); }
            }
            else
            {
                ModelState.AddModelError("1", "Model state is invalid");
                return BadRequest(ModelState);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _repo.DeleteAccountAsync(id);
            if (result > 0)
                return Ok();
            else
                return NotFound();
        }
    }
}
