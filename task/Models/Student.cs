using System.ComponentModel.DataAnnotations;

namespace task.Models
{
    public class Student
    {
        [Key]
        public int Ssn { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string? Image { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
