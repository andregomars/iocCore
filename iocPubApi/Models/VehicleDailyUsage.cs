using System;

namespace iocPubApi.Models
{
    public class VehicleDailyUsage
    {
        public int vid { get; set; }
        public string vname { get; set; }
        public int fid { get; set; }
        public string fname { get; set; }
        public DateTime date { get; set; } 
        public double mileage { get; set; }
        public double soccharged { get; set; }
        public double socused { get; set; }
        public double energycharged { get; set; }
        public double energyused { get; set; }
        public double soc_mile { get; set; }
        public double mile_soc { get; set; }
        public double energy_mile { get; set; }
        public double mile_energy { get; set; }
    }
}
