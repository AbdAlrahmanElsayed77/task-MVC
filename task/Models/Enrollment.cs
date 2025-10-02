using System.ComponentModel.DataAnnotations;

namespace task.Models
{
    public class Enrollment
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        [Range(0, 100)]
        public int Score { get; set; }  
    }
}
