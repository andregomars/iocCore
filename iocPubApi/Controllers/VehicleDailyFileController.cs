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
    public class VehicleDailyFileController : Controller
    {
        private readonly IVehicleDailyFileRepository _repository;

        public VehicleDailyFileController(IVehicleDailyFileRepository repository)
        {
            _repository = repository;
        }

        // GET /api/VehicleDailyFile/{vehicleNames}/{beginDate}/{endDate}
        [HttpGet("GetFileList/{vehicleNames}/{beginDate}/{endDate}")]
        public IEnumerable<VehicleDailyFile> GetFileList(string vehicleNames,
            DateTime beginDate, DateTime endDate)
        {
            var vnames = new string[] {};
            try
            {
                vnames = vehicleNames.Split(',');
            }
            catch
            {}

            //while vehicle name list or begin & end date is invalide
            if (vnames == null || vnames.Length == 0 || beginDate > endDate)
                return new VehicleDailyFile[] {}; 

            return _repository.GetFileList(vnames, beginDate, endDate);
        }

        //GET api/download/12345abc
        [HttpGet("GetFileStream/{fileId}")]
        public async Task<IActionResult> GetFileStream(string fileId) {
            var stream = await {{__get_stream_here__}}
            var response = File(stream, "text/csv"); 
            return response;
        }    
   }
}