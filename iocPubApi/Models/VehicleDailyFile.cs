using System;

namespace iocPubApi.Models
{
    public class VehicleDailyFile
    {
        public int vid { get; set; }
        public string vname { get; set; }
        public int fid { get; set; }
        public string fname { get; set; }
        public int fileid { get; set; }
        public string filename { get; set; }
        public DateTime filetime { get; set; } 
        public DateTime begintime { get; set; } 
        public DateTime endtime { get; set; } 
    }
}
