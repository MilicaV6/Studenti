using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrijemniMVC.Models
{
    public partial class Student
    {
        [Key]
        public int PkStudentId { get; set; }
        [Display(Name ="Broj indeksa")]
        public string BrojIndeksa { get; set; } = null!;
        public string? Ime { get; set; }
        public string? Prezime { get; set; }
        [Range(minimum:1, maximum:4,ErrorMessage ="Godina mora biti između 1 i 4")]
        public int? Godina { get; set; }
       
        public int StatusStudenta { get; set; }
        [ForeignKey(name: "StatusStudenta")]
        public StatusStudenta? StatusStudentaProperty { get; set; }
        public ICollection<StudentKurs> KurseviStudenta { get; set; }
    }
}
