//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClanBattlesService.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Gamer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Gamer()
        {
            this.Members = new HashSet<Member>();
            this.Reservations = new HashSet<Reservation>();
        }
    
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public Nullable<System.DateTime> birthDate { get; set; }
        public string address { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Member> Members { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
