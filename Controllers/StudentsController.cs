using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrijemniMVC;
using PrijemniMVC.Models;
using PrijemniMVC.Models.VM;

namespace PrijemniMVC.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private readonly PrijemniContext _context;
        private readonly IMapper _mapper;


        public StudentsController(PrijemniContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
                   

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.Include(s=>s.StatusStudentaProperty).Include(s=>s.KurseviStudenta)
                .FirstOrDefaultAsync(m => m.PkStudentId == id);
            var model=_mapper.Map<Student,StudentDetailsVM>(student);
            model.ListaKurseva = new List<Kurs>();
            foreach(var kursStudenta in student.KurseviStudenta)
            {
                model.ListaKurseva.Add(_context.Kurs.FirstOrDefault(k => k.PkKursId == kursStudenta.PkKursId));
            }
         
            if (student == null)
            {
                return NotFound();
            }

            return View(model);
        }

       
        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            var statusi = _context.StatusStudenta.ToList();
            StudentInsertUpdateVM model = new StudentInsertUpdateVM();
            var statusiItems = statusi.Select(s => new SelectListItem
            {
                Text = s.NazivStatusa,
                Value = s.PkStatusStudentaId.ToString()
            });
            if (id == null)
            {
                
                model.Statusi = statusiItems;
                return View(model);
            }

            var student = await _context.Students.FirstOrDefaultAsync(s => s.PkStudentId == id);

            if (student == null)
            {
                return NotFound();
            }
            model = _mapper.Map<Student, StudentInsertUpdateVM>(student);
            
            model.Statusi = statusiItems;
            return View(model);
           
        }

      
        [HttpPost]
        public async Task<IActionResult> Upsert(StudentInsertUpdateVM model)
        {
            Student student = _mapper.Map<StudentInsertUpdateVM, Student>(model);
            if (model.PkStudentId == null)
            {

                if (ModelState.IsValid)
                {                   
                    _context.Add(student);                    
                }               
            }
            else
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return View(model);
                    }
                    _context.Update(student);                   
                }
                catch (Exception ex)
                {

                }
    
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
       

       

        // GET: Students/Delete/
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.PkStudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
           
            var studentKursevi = _context.StudentKurs.Where(sk => sk.PkStudentId == id).ToList();
            _context.StudentKurs.RemoveRange(studentKursevi);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.PkStudentId == id);
        }
        [HttpGet, ActionName("Index")]
        public async Task<IActionResult> Index()
        {           
         
            List<StudentSTP> studentInfo = await _context.StudentSTPs.FromSqlRaw("exec stpGetAllStudentInformation").ToListAsync();
            
            return View(studentInfo);           
        }
    }
}
