using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iocPubApi.Models;
using iocPubApi.Repositories;

namespace iocPubApi.Controllers
{
    [Route("[controller]")]
    public class CoreSmsController : Controller
    {
        private readonly ICoreSmsRepository _repository;

        public CoreSmsController(ICoreSmsRepository repository)
        {
            _repository = repository;
        }
        
        [HttpPost]
        public IActionResult Add([FromBody]string vehicleName) 
        {
            if (String.IsNullOrEmpty(vehicleName)) 
                return BadRequest();

            _repository.Add(vehicleName);
            return Ok();
        }
   }

   public class DemoEntity
   {
       public Guid? Id {get; set;}
       public string Name {get; set;}
       public int Age {get; set;}
   }
}
