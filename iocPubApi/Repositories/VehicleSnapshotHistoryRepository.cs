using System;
using System.IO;
using System.Collections.Generic;
using iocPubApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Chilkat;

namespace iocPubApi.Repositories
{
    public class VehicleSnapshotHistoryRepository : IVehicleSnapshotHistoryRepository
    {
        private Csv csv;
        private string folder; //= "/Users/andre/projects/iocCore/res/pubapi/csv";
        public IConfigurationRoot Configuration { get; }

        public VehicleSnapshotHistoryRepository()
        {
            Configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json").Build();

            folder = Configuration["HAMS.Api:Directory.SMSData"];
        }

        IEnumerable<VehicleSnapshot> IVehicleSnapshotHistoryRepository.GetByVehicleName(string vname)
        {
            csv = new Csv();
            csv.HasColumnNames = true;

            bool success = csv.LoadFile(folder + "/snapshot.csv");
            if(!success) return null;
            var snapshots = new List<VehicleSnapshot>();
            int rowCount = csv.NumRows;
            int colCount = csv.NumColumns;
            for(int i = 0; i < rowCount; i++)
            {
                snapshots.Add(new VehicleSnapshot{
                    code = csv.GetCellByName(i, "code"),
                    name = csv.GetCellByName(i, "name"),
                    value = Convert.ToDouble(csv.GetCellByName(i, "value")),
                    unit = csv.GetCellByName(i, "unit"),
                    time = Convert.ToDateTime(csv.GetCellByName(i, "time"))
                });
            }

            return snapshots;

        }

                
        IEnumerable<VehicleSnapshot> IVehicleSnapshotHistoryRepository.GetWholeDayByVehicleName(string vname, DateTime date)
        {
            throw new NotImplementedException();
       }
   }
}