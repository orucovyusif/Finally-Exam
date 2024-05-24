using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finally_Exam.Models
{
    public class Explore
    {
        public int Id { get; set; }
        [MinLength(3)]
        [MaxLength(20)]
        public string Name { get; set; }
        [MinLength(3)]
        [MaxLength(20)]
        public string Position {  get; set; }
        [MinLength(3)]
        [MaxLength(20)]
        public string Description { get; set; }
        public string? ImgUrl {  get; set; }

        [NotMapped]
        public IFormFile ImgFile { get; set; }
    }
}
