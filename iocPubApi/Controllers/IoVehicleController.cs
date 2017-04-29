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
    public class IoVehicleController : Controller
    {
        private readonly IIoVehicleRepository _repository;

        public IoVehicleController(IIoVehicleRepository repository)
        {
            _repository = repository;
        }
        
        // GET api/IoFleet
        [HttpGet]
        public IEnumerable<IoVehicle> GetAll()
        {
            return _repository.GetAll();
        }
   }
}
