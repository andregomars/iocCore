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
    public class HamsNetDataItemController : Controller
    {
        private readonly IHamsNetDataItemRepository _repository;

        public HamsNetDataItemController(IHamsNetDataItemRepository repository)
        {
            _repository = repository;
        }
        
        // GET api/hamsnetdataitem
        [HttpGet]
        public IEnumerable<HamsNetDataItem> GetAll()
        {
            return _repository.GetAll();
        }
   }
}
