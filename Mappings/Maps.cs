using AutoMapper;
using PrijemniMVC.Models;
using PrijemniMVC.Models.VM;

namespace PrijemniMVC.Mappings

{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<Student,StudentInsertUpdateVM>().ReverseMap();
            CreateMap<Student, StudentDetailsVM>().ReverseMap();
            CreateMap<Kurs, CreateKursVM>().ReverseMap();

        }
    }
}
