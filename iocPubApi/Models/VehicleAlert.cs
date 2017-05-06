using System;

namespace iocPubApi.Models
{
    public class VehicleAlert
    {
        public int vid { get; set; }
        public string vname { get; set; }
        public int fid { get; set; }
        public string fname { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public double value { get; set; }
        public string unit { get; set; }
        public DateTime? updated { get; set; }
    }
}