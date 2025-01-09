namespace StudentsForStudents.Models
{
    public class ChatViewModel
    {
        public List<Chat> ChatHistory { get; set; }
        public Chat CurrentChat { get; set; }
        public List<Chat> PreviousChats { get; set; } 

    }

}
