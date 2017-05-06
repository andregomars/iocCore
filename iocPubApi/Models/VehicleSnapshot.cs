using System;

namespace iocPubApi.Models
{
    public class VehicleSnapshot
    {
        public int vid { get; set; }
        public string vname { get; set; }
        public int fid { get; set; }
        public string fname { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public double value { get; set; }
        public string unit { get; set; } 
    }
}

/*
itemcode	itemname
1G	Lowest Battery Temp
1I	Charging Status
1L	Motor Voltage
1F	Total Voltage
2H	Range
1D	Speed
1M	WAVE Status
2N	Right Charging Gun
2F	Total Current
1H	Total Mileage
2G	Highest Battery Temp
1J	Battery Energy
2L	Motor RPM
1N	Left Charging Gun
1E	SOC
1K	High Voltage
 */
