using System;

namespace iocPubApi.Models
{
    public class VehicleSnapshot
    {
        public string code { get; set; }
        public string name { get; set; }
        public double value { get; set; }
        public string unit { get; set; } 
        public DateTime time { get; set; }
    }
}

/* Current codes
Item	SMS code	Daily code
		
SOC	2A	2a
Battery Energy	2B	2b
kWh Charged	2C	2c
kWh Used	2D	2d
Battery Current	2E	2e
Battery Voltage	2F	2f
Lowest Battery Temp	2G	2g
Highest Battery Temp	2H	2h
Vehicle Speed	2I	2i
Engine Speed	2J	2j
Total Mileage	2K	2k
Range	2L	2l
L Charging Status	2M	2m
R Charging Status	2N	2n
L Charging Gun	2O	2o
R Charging Gun	2P	2p
Charging Hour	2Q	2q
Charging Minute	2R	2r
Override status	2S	2s
WAVE Status	2T	2t
High Voltage / OK Signal	2U	2u
Gear Status	2V	2v
Front Door	2W	2w
Rear Door	2X	2x
Seat Belt Status	2Y	2y
Motor Voltage	2Z	2z
Start button	3A	3a
Speed status	3B	3b

 */

/* obsolete
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
