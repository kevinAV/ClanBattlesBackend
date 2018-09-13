using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClanBattlesService.Dtos
{
    public class MemberInfo
    {
        public int id { get; set; }
        
        public ClanInfo clan { get; set; }

        public GamerInfo gamer { get; set; }

        public string status { get; set; }
    }
}