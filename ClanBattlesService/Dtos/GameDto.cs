using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClanBattlesService.Dtos
{
    public class GameDto
    {
        public int id { get; set; }

        [Required]
        [MaxLength(200)]
        public string name { get; set; }

        [Required]
        [MaxLength(500)]
        public string description { get; set; }

        [Required]
        [MaxLength(500)]
        public string urlWebPage { get; set; }

        [MaxLength(500)]
        public string urlToImage{ get; set; }

        public float rating { get; set; }


        [Required(AllowEmptyStrings = false)]
        [StringLength(3)]
        public string status { get; set; }
    }
}