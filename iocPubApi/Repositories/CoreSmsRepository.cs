using iocPubApi.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Dapper;
using System.Data;

namespace iocPubApi.Repositories
{
    public class CoreSmsRepository: ICoreSmsRepository
    {
        private readonly io_onlineContext db;

        public CoreSmsRepository(io_onlineContext context)
        {
            db = context;
        }

        public void Add(string vname)
        {
            var conn = db.Database.GetDbConnection();
            var statusList = conn.Execute("dbo.UP_HAMS_AddSMSReceivingRequestByVehicle", 
                new { VehicleName = vname},
                commandType: CommandType.StoredProcedure);
            
        }
    }
}