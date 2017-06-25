using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using iocPubApi.Models;
using iocPubApi.Repositories;

namespace iocPubApi.Controllers
{
    [Route("[controller]")]
    public class PingController : Controller
    {
        private readonly IPingRepository _repository;

        public PingController(IPingRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public string Ping()
        {
            return "Pong";
        }

        [HttpGet("Database")]
        public string PingDB()
        {
            return _repository.PingDB();
        }
    }
}