using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrijemniMVC.Models
{
    public partial class Kurs
    {
        [Key]
        public int PkKursId { get; set; }
        [Required]
        public string NazivKursa { get; set; }
        public ICollection<StudentKurs> StudentiNaKursu { get; set; }
    }
}
