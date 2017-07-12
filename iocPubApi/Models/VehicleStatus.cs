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
        public double axisx { get; set; }
        public double axisy { get; set; }
        public double axisz { get; set; }
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
        public double highvoltagestatus { get; set; }
        public double actualdistance { get; set; }
        public DateTime? updated { get; set; }
    }

    public class VehicleStatusWithCode
    {
        public int vid { get; set; }
        public string vname { get; set; }
        public int fid { get; set; }
        public string fname { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public double axisx { get; set; }
        public double axisy { get; set; }
        public double axisz { get; set; }
        public string itemcode { get; set; }
        public string itemname { get; set; }
        public double value { get; set; }
        public string unit { get; set; }
        public DateTime? realTime { get; set; }
    }
}