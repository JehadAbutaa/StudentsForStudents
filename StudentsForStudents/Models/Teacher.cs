namespace StudentsForStudents.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string StudentId { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        public string Desc { get; set; }

        public string ImgPath { get; set; }
        public string QualificationCourses { get; set; }
        public string Major { get; set; }
        public int Level { get; set; }

        public float GPA { get; set; }

        public int Rate { get; set; }
        public virtual ICollection<Courses> InrolmentCourses { get; set; } = new List<Courses>();



    }
}
