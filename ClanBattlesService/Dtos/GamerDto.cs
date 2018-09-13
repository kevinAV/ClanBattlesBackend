using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClanBattlesService.Dtos
{
    public class GamerDto
    {
        public int id { get; set; }

        [Required]
        [MaxLength(200)]
        public string firstName { get; set; }

        [Required]
        [MaxLength(200)]
        public string lastname { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [DataType(DataType.Date)]
        public DateTime birthDate { get; set; }

        [Required]
        [MaxLength(500)]
        public string address { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(3)]
        public string status { get; set; }
    }
}