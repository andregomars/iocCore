using System;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using iocPubApi.Models;
using iocPubApi.Repositories;
using System.IO;
using System.Net.Http.Headers;

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

        // [HttpGet("GetFile/{fileId}")]
        // public HttpResponseMessage GetFile(int fileId)
        // {
        //     string path = _repository.GetDailyFilePath(fileId);
        //     if (String.IsNullOrEmpty(path)) 
        //         return new HttpResponseMessage(HttpStatusCode.NotFound);

        //     FileInfo info = new FileInfo(path);
        //     if (!info.Exists)
        //         return new HttpResponseMessage(HttpStatusCode.NotFound);
                 
        //     // var fileName = Path.GetFileName(path);
        //     var file = new FileStream(path, FileMode.Open, FileAccess.Read);
        //     var response = new HttpResponseMessage(HttpStatusCode.OK);
        //     response.Content = new StreamContent(file);
        //     response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        //     response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
        //     response.Content.Headers.ContentDisposition.FileName = info.Name;


        //     return response;
        // }

        [HttpGet("GetFileStream/{fileId}")]
        public async Task<IActionResult> GetFileStream(int fileId)
        {
            string path = _repository.GetDailyFilePath(fileId);
            if (String.IsNullOrEmpty(path)) 
                return NotFound();

            FileInfo info = new FileInfo(path);
            if (!info.Exists)
                return NotFound();
                 
            var file = new FileStream(path, FileMode.Open, FileAccess.Read);
            var result = new byte[file.Length];
            await file.ReadAsync(result, 0, (int)file.Length);
            var response = File(result, "text/csv", info.Name);

            return response;
        }

    }    
}