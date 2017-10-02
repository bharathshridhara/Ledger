using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ledger.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ledger.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private ILedgerRepository _repo;
        //private IMapper _mapper;
        //private IVMFactory _vmFactory;

        public AuthController(ILedgerRepository repo)
        {
            _repo = repo;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            var user =  _repo.GetUserDetails(HttpContext.User.Identity.Name);
            if (user != null)
                return Ok(user);
            else
                return NotFound();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserProfile value)
        {
            value.Id = 0;
            var result = await _repo.AddUserAsync(value);
            if (result > 0)
                return Created("", value);
            else
                return BadRequest();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
