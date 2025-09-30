namespace task.ViewModels
{
    public class DepartmentVM
    {
        public string Name { get; set; }
        public string Manager { get; set; }
        public int StdCount { get; set; }
        public int InsCount { get; set; }
        public List<string> InsNames { get; set; }
        public List<string> StdNames { get; set; }
    }
}
