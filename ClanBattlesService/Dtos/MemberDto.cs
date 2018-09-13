using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClanBattlesService.Dtos
{
    public class MemberDto
    {
        public int id { get; set; }

        [Required]
        public int clanId { get; set; }

        [Required]
        public int gamerId {get;set;}

        [Required(AllowEmptyStrings = false)]
        [StringLength(3)]
        public string status { get; set; }

    }
}