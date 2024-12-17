using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Org.BouncyCastle.Tls;
using StudentsForStudents.Context;
using StudentsForStudents.Models;

namespace StudentsForStudents.Controllers
{
    public class ChatHub : Hub
    {
        private readonly SFSDBContect _context;

        public ChatHub(SFSDBContect context)
        {
            _context = context;
        }

        public async Task SendMessage(string senderId, string receiverId, string message, string SenderName, string ReceiverName)
        {
            // Save the message to the database
            var chat = new Chat
            {
                SenderID = senderId,
                SenderName = SenderName,
                ReceiverId = receiverId,
                ReceiverName = ReceiverName,
                Message = message,
                DateTime = DateTime.UtcNow,
                IsRead = false
            };
            _context.Chats.Add(chat);

            await _context.SaveChangesAsync();

            // Notify the receiver in real-time
            await Clients.User(receiverId).SendAsync("ReceiveMessage", SenderName, message);
        }

        

        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;

            // Fetch unread messages
            var unreadMessages = _context.Chats
                .Where(m => m.ReceiverId == userId && !m.IsRead)
                .ToList();

            // Notify the user of unread messages
            foreach (var message in unreadMessages)
            {
                await Clients.Caller.SendAsync("ReceiveMessage", message.SenderID, message.Message);
            }

            await base.OnConnectedAsync();
        }



    }

}
