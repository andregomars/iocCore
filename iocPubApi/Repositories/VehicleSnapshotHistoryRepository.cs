using System;
using System.IO;
using System.Collections.Generic;
using iocPubApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Chilkat;

namespace iocPubApi.Repositories
{
    public class VehicleSnapshotHistoryRepository : IVehicleSnapshotHistoryRepository
    {
        private Csv csv;
        private readonly string folder = "/Users/andre/projects/iocCore/res/pubapi/csv";

        public VehicleSnapshotHistoryRepository()
        {
        }

        IEnumerable<VehicleSnapshot> IVehicleSnapshotHistoryRepository.GetByVehicleName(string vname)
        {
            csv = new Csv();
            csv.HasColumnNames = true;

            bool success = csv.LoadFile(folder + "/snapshot.csv");
            if(!success)
            {
                return null;
            }

            // VehicleSnapshot snapshot = new VehicleSnapshot {
            //     code = "1E",
            //     name = success.ToString(),
            //     value = 80,
            //     unit = "%",
            //     time = new DateTime()
            // };

            var snapshot = new List<VehicleSnapshot>();

            int rowCount = csv.NumRows;
            int colCount = csv.NumColumns;
            for(int i = 0; i < rowCount; i++)
            {
                snapshot.Add(new VehicleSnapshot{
                    code = csv.GetCellByName(i, "code"),
                    name = csv.GetCellByName(i, "name"),
                    value = Convert.ToDouble(csv.GetCellByName(i, "value")),
                    unit = csv.GetCellByName(i, "unit"),
                    time = Convert.ToDateTime(csv.GetCellByName(i, "time"))
                });
            }

            return snapshot;

        }

                
        IEnumerable<VehicleSnapshot> IVehicleSnapshotHistoryRepository.GetWholeDayByVehicleName(string vname, DateTime date)
        {
            throw new NotImplementedException();
       }
   }
}