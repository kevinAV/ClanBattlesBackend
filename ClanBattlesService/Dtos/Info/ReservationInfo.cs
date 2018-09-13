using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClanBattlesService.Dtos.Info
{
    public class ReservationInfo
    {
        public int id { get; set; }
        public GamerDto gamer { get; set; }
        public LanCenterDto lanCenter { get; set; }
        public string description { get; set; }
        public DateTime startDate { get; set; }
        public int numberHours { get; set; }
        public bool isApproved { get; set; }
        public string status { get; set; }
    }
}