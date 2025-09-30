using System.ComponentModel.DataAnnotations;

namespace task.Models
{
    public class Course
    {
        [Key]
        public int Num { get; set; }
        public string Name { get; set; }
        public string Topic { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        // Navigatio
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Teaches> Teaches { get; set; }
    }
}

