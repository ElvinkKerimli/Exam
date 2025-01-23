using Exam.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Exam.Models
{
    public class Duty:BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public List<Doctor> Doctors { get; set; }
    }
}
