using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrijemniMVC.Models
{
    public partial class StatusStudenta
    {
        public int PkStatusStudentaId { get; set; }
        [Display(Name ="Status")]
        public string NazivStatusa { get; set; } = null!;
    }
}
