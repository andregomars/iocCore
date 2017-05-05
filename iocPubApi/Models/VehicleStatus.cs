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

/*
spn	spnname	value	unit
917	Total mileage	6237.9	miles
84	Vehicle speed	0	mph
9007	Range	253	miles
190	Engine speed	0	rpm
9012	Engine hour	317.7	h
9013	Idel time	95	h
9002	Voltage	25.6	V
9005	Engine temp	65	F
9006	Coolant temp	55	F
4001	SOC	80	A
4002	Current	20	A
4003	Remaining energy	12	kWh
*/