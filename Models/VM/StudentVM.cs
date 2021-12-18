using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrijemniMVC.Models.VM
{
    public class StudentInsertUpdateVM
    {
      
        public int PkStudentId { get; set; }
        [Display(Name = "Broj Indeksa")]
        [Required]
        public string BrojIndeksa { get; set; }
        public string? Ime { get; set; } 
        public string? Prezime { get; set; }
        [Range(minimum: 1, maximum: 6,ErrorMessage ="Godina should be between  1 and 6")]
        public int? Godina { get; set; }
        [Display(Name ="Status")]
        [Required]
        public int StatusStudenta { get; set; }
             
        public IEnumerable<SelectListItem>? Statusi { get; set; }
    }

    public class StudentiNaKursuVM
    {

        public int PkStudentId { get; set; }
        [Display(Name = "Broj Indeksa")]
        [Required]
        public string BrojIndeksa { get; set; }
        public string? Ime { get; set; }
        public string? Prezime { get; set; }
       
        public int? Godina { get; set; }
        [Display(Name = "Status")]
        [Required]
        public int StatusStudenta { get; set; }

        public StatusStudenta? StatusStudentaProperty { get; set; } = null!;
       
    }
    public class StudentDetailsVM
    {
        public int PkStudentId { get; set; }
        [Display(Name = "Broj Indeksa")]
        [Required]
        public string BrojIndeksa { get; set; }
        public string? Ime { get; set; }
        public string? Prezime { get; set; }
      
        public int? Godina { get; set; }
        [Display(Name = "Status")]
        public StatusStudenta? StatusStudentaProperty { get; set; } = null!;
        public List<Kurs> ListaKurseva { get; set; }

    }

}
