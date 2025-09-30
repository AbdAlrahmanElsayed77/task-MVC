using System.ComponentModel.DataAnnotations;

namespace task.Models
{
    public class Instructor
    {
        [Key]
        public int SSN { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; }
        public int Age { get; set; }
        public string Degree { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection<Teaches> Teaches { get; set; }
    }
}
