using Exam.DAL;
using Exam.Models;
using Exam.ViewModels.Duty;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exam.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DutyController : Controller
    {
        AppDbContext db;

        public DutyController(AppDbContext db)
        {
            this.db = db;
        }

        public async  Task<IActionResult> Index()
        {
            var duty = await db.Dutys.Include(x => x.Doctors).ToListAsync();
            return View(duty);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DutyVm duty)
        {
            if (duty == null)
            {
                ModelState.AddModelError("duty", "Zehmet olmasa Mlumatlari daxil edin");
            }
            if(!ModelState.IsValid)
            {
                return View();
            }
            var duties = new Duty()
            {
                Name = duty.Name,
            };
            db.Dutys.Add(duties);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            var duty = db.Dutys.FirstOrDefault(x => x.Id == id);
            if (duty == null) {return View();}
            return View(duty);

        }
        [HttpPost]
        public IActionResult Update(DutyVm duty)
        {
            if (duty == null)
            {
                return View();
            }
            if (!ModelState.IsValid)
            {
                return View(duty);
            }
            var duties = new Duty()
            {
                Name = duty.Name,
            };
            db.Dutys.Update(duties);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var duty = db.Dutys.FirstOrDefault( x=>x.Id == id);
            if (duty == null)
            {
                    
                return View();
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            db.Dutys.Remove(duty);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
