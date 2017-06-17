using System;
using System.Collections.Generic;
using iocPubApi.Models;

namespace iocPubApi.Repositories
{
    public interface IVehicleDailyFileRepository
    {
        IEnumerable<VehicleDailyFile> GetFileList(string[] vnames, 
            DateTime beginDate, DateTime endDate);

        string GetDailyFilePath(int fileId);
    }
}