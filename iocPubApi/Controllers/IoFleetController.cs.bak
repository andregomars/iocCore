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
    public class IoFleetController : Controller
    {
        private readonly IIoFleetRepository _repository;

        public IoFleetController(IIoFleetRepository repository)
        {
            _repository = repository;
        }
        
        // GET api/IoFleet
        [HttpGet]
        public IEnumerable<IoFleet> GetAll()
        {
            return _repository.GetAll();
        }
   }
}
