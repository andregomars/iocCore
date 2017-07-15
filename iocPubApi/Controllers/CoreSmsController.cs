using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iocPubApi.Models;
using iocPubApi.Repositories;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace iocPubApi.Controllers
{
    [Route("[controller]")]
    public class CoreSmsController : Controller
    {
        private readonly ICoreSmsRepository _repository;
        private ILogger<CoreSmsController> _logger;

        public CoreSmsController(ICoreSmsRepository repository,
            ILogger<CoreSmsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        
        [HttpPost]
        public IActionResult Add([FromBody]string vehicleName) 
        {
            _logger.LogInformation("call function to add CoreSMS");
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
