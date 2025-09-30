namespace task.Models
{
    public class Department
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public String Manger { get; set; }

        public ICollection<Instructor> Instructors { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
