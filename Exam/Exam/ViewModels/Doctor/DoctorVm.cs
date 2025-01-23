using Exam.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exam.ViewModels.Doctor
{
    public class DoctorVm
    {
        public string Name { get; set; }        
        public int DutyId { get; set; }
        public string ImgUrl { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
