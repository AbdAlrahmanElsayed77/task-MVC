using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace task.Models
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Num { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Topic { get; set; }

        [Range(0, 100)]
        public int Degree { get; set; }          

        [Range(0, 100)]
        public int MinDegree { get; set; }      

      
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<Teaches> Teaches { get; set; } = new List<Teaches>();
    }
}
