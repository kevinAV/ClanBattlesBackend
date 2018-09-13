using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClanBattlesService.Dtos
{
    public class ClanDto
    {
        
        public int id { get; set; }

        public GameInfo game { get; set;}

        [Required]
        [MaxLength(200)]
        public string name { get; set; }

        [MaxLength(800)]
        public string description { get; set; }

        public int rating { get; set; }

        public int win { get; set; }

        public int lose { get; set; }

        public float winRate { get; set; }

        [MaxLength(500)]
        public string urlToImage { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(3)]
        public string status { get; set; }
    }
}