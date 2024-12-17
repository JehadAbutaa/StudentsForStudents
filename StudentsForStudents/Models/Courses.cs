namespace StudentsForStudents.Models
{
    public class Courses
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string ImgPath { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}
