namespace StudentsForStudents.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int RatingValue { get; set; }
        public Teacher Teacher { get; set; }
    }
}
