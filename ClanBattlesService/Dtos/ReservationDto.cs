using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClanBattlesService.Dtos
{
    public class ReservationDto
    {
        public int id { get; set; }
        public int gamerId  { get; set; }
        public int lanCenterId { get; set; }
        public string description { get; set; }
        public DateTime startDate { get; set; }
        public int numberHours { get; set; }
        public bool isApproved { get; set; }
        public string status { get; set; }

    }
}