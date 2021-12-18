using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrijemniMVC.Models
{
    public partial class StudentKurs
    {
        public int PkStudentId { get; set; }
        [ForeignKey(name: "PkStudentId")]
        public Student Student { get; set; }
        public int PkKursId { get; set; }
        [ForeignKey(name: "PkKursId")]
        public Kurs Kurs { get; set; }
    }
}
