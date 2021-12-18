using System.ComponentModel.DataAnnotations;

namespace PrijemniMVC.Models
{
    public class StudentSTP
    {
        public int PkStudentId { get; set; }
        [Display(Name = "Broj Indeksa")]
        public string BrojIndeksa { get; set; } = null!;
        public string? Ime { get; set; } = null!;
        public string? Prezime { get; set; } = null!;
        public int? Godina { get; set; }
        public int PkStatusStudentaId { get; set; }
        [Display(Name ="Status")]
        public String NazivStatusa { get; set; }

    }
}
