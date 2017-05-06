using System;

namespace iocPubApi.Models
{
    public class VehicleStatus
    {
        public int vid { get; set; }
        public string vname { get; set; }
        public int fid { get; set; }
        public string fname { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public double soc { get; set; }
        public double status { get; set; }
        public double range { get; set; }
        public double mileage { get; set; }
        public double voltage { get; set; }
        public double current { get; set; }
        public double temperaturehigh { get; set; }
        public double temperaturelow { get; set; }
        public double speed { get; set; }
        public double remainingenergy { get; set; }
        public DateTime? updated { get; set; }
    }
}