using System.ComponentModel.DataAnnotations;

namespace task.Models
{
    public class Department
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Manger { get; set; }

        [Required]
        [RegularExpression("^(EG|USA)$", ErrorMessage = "Location must be either 'EG' or 'USA'")]
        public string Location { get; set; }

        public ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
