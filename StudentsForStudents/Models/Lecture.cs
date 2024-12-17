namespace StudentsForStudents.Models
{
    public class Lecture
    {
        public int Id { get; set; }
        public string TeacherName { get; set; }
        public string StudentName { get; set; }
        
        public string Topic { get; set; }
        public DateTime Time { get; set; }

        public string Type { get; set; } //Online Or Not
        public int StudentId { get; set; }
        public int TeacherId { get; set; }

    }
}
