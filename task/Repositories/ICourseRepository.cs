using task.Models;

namespace task.Repositories
{
    public interface ICourseRepository
    {
        IEnumerable<Course> GetAll();
        Course? GetById(int id);
        void Add(Course course);
        void Update(Course course);
        void Delete(int id);
        bool ExistsByName(string name, int? excludeId = null);
        void Save();
    }
}
