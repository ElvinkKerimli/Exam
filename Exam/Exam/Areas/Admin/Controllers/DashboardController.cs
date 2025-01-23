using Exam.DAL;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        AppDbContext db;

        public DashboardController(AppDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            
            return View();
        }
    }
}
