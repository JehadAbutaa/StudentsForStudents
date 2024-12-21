namespace StudentsForStudents.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string StudentId { get; set;}

        public string Major {  get; set; }

        public byte[] ProfilePicture { get; set; } 
        public int Level { get; set;}

         
    }
}
