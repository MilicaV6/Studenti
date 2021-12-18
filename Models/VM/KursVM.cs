using System.ComponentModel.DataAnnotations;

namespace PrijemniMVC.Models.VM
{
    public class CreateKursVM
    {
        [Required]
        [Display(Name = "Naziv Kursa")]
        public string NazivKursa { get; set; }

        public List<int>? StudentiIds { get; set; }
      

    }
    public class CreateKursGetVM
    {
        [Required]
        [Display(Name ="Naziv Kursa")]
        public string NazivKursa { get; set; }

        public List<int>? StudentiIds { get; set; }


        public List<Student> Studenti { get; set; }

    }
}
