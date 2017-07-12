using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iocPubApi.Models;
using iocPubApi.Repositories;

namespace iocPubApi.Controllers
{
    [Route("api/[controller]")]
    public class CoreSmsController : Controller
    {
        private readonly ICoreSmsRepository _repository;

        public CoreSmsController(ICoreSmsRepository repository)
        {
            _repository = repository;
        }
        
        [HttpPost]
        public IActionResult Add([FromBody]CoreSms sms) 
        // public IActionResult Add([FromBody]string sms) 
        {
            if (sms == null) return BadRequest();

            _repository.Add(sms);
            return Ok();
        }
   }
}
