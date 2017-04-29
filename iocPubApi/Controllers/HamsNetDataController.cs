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
    public class HamsNetDataController : Controller
    {
        private readonly IHamsNetDataRepository _repository;

        public HamsNetDataController(IHamsNetDataRepository repository)
        {
            _repository = repository;
        }
        
        // GET api/hamsnetdata
        [HttpGet]
        public IEnumerable<HamsNetData> GetAll()
        {
            return _repository.GetAll();
        }
   }
}
