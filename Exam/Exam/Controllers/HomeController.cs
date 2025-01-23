
using Exam.DAL;
using Exam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Exam.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext db;

        public HomeController(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<IActionResult> Index()
        {
           var doctor= await db.Doctors.Include(x => x.Duty).ToListAsync();
            return View(doctor);
        }
        public async Task<IActionResult> Doctors()
        {
            var doctor = await db.Doctors.Include(x => x.Duty).ToListAsync();
            return View(doctor);
        }
    }
}
