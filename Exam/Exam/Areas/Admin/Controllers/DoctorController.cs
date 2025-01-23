using Exam.DAL;
using Exam.Helpers.Extentions;
using Exam.Models;
using Exam.ViewModels.Doctor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Security.Cryptography.Xml;

namespace Exam.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DoctorController : Controller
    {
       
            private readonly IWebHostEnvironment env;
            AppDbContext _dbcontext;



            public DoctorController(AppDbContext dbcontext, IWebHostEnvironment env)
            {
                _dbcontext = dbcontext;
                this.env = env;
            }
            public async Task<IActionResult> Index()
            {
                List<Doctor> members = await _dbcontext.Doctors.Include(x=>x.Duty).ToListAsync();
                return View(members);
            }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DoctorVm member)
        {
            if (member.FormFile == null)
            {
                ModelState.AddModelError("File", "Fayl daxil edin");
                return View();
            }
            if (!member.FormFile.ContentType.Contains("image"))
            {
                ModelState.AddModelError("File", "Duzgun file novu elave edin");
                return View();
            }
            if (member.FormFile.Length > 2097152)
            {
                ModelState.AddModelError("FIle", "Sekil olcusu boyukdur");
                return View();
            }
            member.ImgUrl = member.FormFile.Upload(env.WebRootPath, "Admin/Upload/Members");
            if (!ModelState.IsValid)
            {
                return View();
            }
            var members = new Doctor()
            {
                Name = member.Name,
                DutyId = member.DutyId,
                ImgUrl = member.ImgUrl,


            };
            _dbcontext.Doctors.Add(members);
            _dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }
                var member = _dbcontext.Doctors.FirstOrDefault(c => c.Id == id);
                if (member == null) { return NotFound(); }
                FileExtention.DeleteFile(env.WebRootPath, "Admin/Upload/Members", member.ImgUrl);
                _dbcontext.Doctors.Remove(member);
                _dbcontext.SaveChanges();
                return RedirectToAction("Index");
            }
            public IActionResult Update(int id)
            {
                if (id == null)
                {
                    return NotFound();
                }
                var member = _dbcontext.Doctors.FirstOrDefault(c => c.Id == id);
                if (member == null) { return NotFound(); }
                return View(member);
            }
            [HttpPost]
            public async Task<IActionResult> Update(Doctor member)
            {
                if (!ModelState.IsValid)
                {
                    return View(member);
                }
                if (member.Id == null)
                {
                    return NotFound();
                }
                var olddoctor =await  _dbcontext.Doctors.FirstOrDefaultAsync(x=>x.Id == member.Id);
                if(member.FormFile != null)
                {
                    FileExtention.DeleteFile(env.WebRootPath, "Admin/Upload/Members",olddoctor.ImgUrl );
					member.ImgUrl = member.FormFile.Upload(env.WebRootPath, "Admin/Upload/Members");
				}
                var oldproduct = _dbcontext.Doctors.FirstOrDefault(c => c.Id == member.Id);
                if (oldproduct == null) { return NotFound(); }
                oldproduct.Name = member.Name;  
                oldproduct.ImgUrl = member.ImgUrl;
                _dbcontext.SaveChanges();
                return RedirectToAction("Index");
            }
        
    }
}
