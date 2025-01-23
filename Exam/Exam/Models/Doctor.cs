using Exam.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exam.Models
{
    public class Doctor:BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int DutyId { get; set; }
        public Duty Duty { get; set; }
        [Required]
        public string ImgUrl { get; set; }
        [NotMapped]
        public IFormFile FormFile { get; set; }
    }
}
