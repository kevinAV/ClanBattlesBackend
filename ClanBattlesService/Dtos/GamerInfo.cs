using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClanBattlesService.Dtos
{
    public class GamerInfo
    {
        public int id { get; set; }

       
        public string firstName { get; set; }

       
        public string lastname { get; set; }

       
        public string email { get; set; }


        public DateTime birthDate { get; set; }

   
        public string address { get; set; }

      
        public string status { get; set; }
    }
}