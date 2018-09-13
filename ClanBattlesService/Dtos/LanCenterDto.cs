using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClanBattlesService.Dtos
{
    public class LanCenterDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string ruc { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }
    }
}