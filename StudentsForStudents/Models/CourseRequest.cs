namespace StudentsForStudents.Models
{
    public class CourseRequest
    {
        public int RequestId { get; set; }
        public string CourceName { get; set; }
        public string Description { get; set; }

        public string Faculty {  get; set; }
        public string RequestedBy { get; set; }
        public string Status { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
