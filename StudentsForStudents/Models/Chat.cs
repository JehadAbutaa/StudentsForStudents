using System;

namespace StudentsForStudents.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string SenderID { get; set; }
        public string SenderName { get; set; }
        public string ReceiverId { get; set;}

        public string ReceiverName { get; set; }
        public string Message { get; set; }

        public DateTime DateTime  { get; set; }

        public bool IsRead { get; set; }
    }
}
