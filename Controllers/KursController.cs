using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrijemniMVC.Models;
using PrijemniMVC.Models.VM;

namespace PrijemniMVC.Controllers
{
    [Authorize]
    public class KursController : Controller
    {
        private readonly PrijemniContext _context;
        private readonly IMapper _mapper;
        public KursController(PrijemniContext context, IMapper mapper)
        {
            _context = context;
            _mapper=mapper;
        }
        // GET: KursController
        public async Task<ActionResult> Index()
        {
            List<Kurs> kursevi = await _context.Kurs.ToListAsync();
            return View(kursevi);
        }
        [HttpGet]
        public async Task<IActionResult> GetStudentiNaKursu(int idKursa)
        {
           var kurs = await _context.Kurs.Include(k=>k.StudentiNaKursu).FirstOrDefaultAsync(k => k.PkKursId == idKursa);
            List<Student> model = new List<Student>();
            foreach (var studentNaKursu in kurs.StudentiNaKursu)
            {
                model.Add(_context.Students.Include(s=>s.StatusStudentaProperty).FirstOrDefault(s=>s.PkStudentId== studentNaKursu.PkStudentId));
            }
            return View(model);
        }

       

        // GET: KursController/Create
        public async Task<ActionResult> Create()
        {
            CreateKursGetVM model=new CreateKursGetVM();
            model.Studenti= await _context.Students.ToListAsync();         
            return View(model);
        }

        // POST: KursController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult>Create([FromForm] CreateKursVM kursVM)
        {
        
            try
            {
                if(ModelState.IsValid)
                {
                   
                    Kurs kurs = _mapper.Map<CreateKursVM, Kurs>(kursVM);
                    kurs.StudentiNaKursu = new List<StudentKurs>();
                    foreach (var item in kursVM.StudentiIds)
                    {
                        kurs.StudentiNaKursu.Add(new StudentKurs {
                            
                            PkStudentId = item,
                            PkKursId=kurs.PkKursId
                        
                        });
                    }
                   _context.Kurs.Add(kurs);
                   await _context.SaveChangesAsync();
                   
                }
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                return View();
            }
        }

       
      
       

        // POST: KursController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int idKursa)
        {
            try
            {
                Kurs kurs = await _context.Kurs.FirstOrDefaultAsync(k => k.PkKursId == idKursa);
                _context.Kurs.Remove(kurs);
                var studentKursevi = _context.StudentKurs.Where(sk => sk.PkKursId == idKursa).ToList();
                _context.StudentKurs.RemoveRange(studentKursevi);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
