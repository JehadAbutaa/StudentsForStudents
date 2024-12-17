using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;
using StudentsForStudents.Context;
using StudentsForStudents.Controllers;
using StudentsForStudents.Models;

public class ChatController : Controller
{
    private readonly SFSDBContect _context;
    private readonly IHubContext<ChatHub> _hubContext;

    public ChatController(SFSDBContect context, IHubContext<ChatHub> hubContext)
    {
        _context = context;
        _hubContext = hubContext;
    }

    // [HttpPost]
    // public async Task<IActionResult> SendMessage(string senderId, string senderName, string receiverId, string receiverName, string message)
    // {
    //     // Save the message to the database
    //     var chat = new Chat
    //     {
    //         SenderID = senderId,
    //         SenderName = senderName,
    //         ReceiverId = receiverId,
    //         ReceiverName = receiverName,
    //         Message = message,
    //         DateTime = DateTime.UtcNow,
    //         IsRead = false
    //     };
    //
    //     _context.Chats.Add(chat);
    //     await _context.SaveChangesAsync();
    //
    //     // Notify the receiver using SignalR
    //     await _hubContext.Clients.User(receiverId).SendAsync("ReceiveMessage", senderName, message);
    //
    //     return Ok(new { success = true });
    // }
    //
    // public async Task<IActionResult> ChatWindow(string receiverId, string receiverName)
    // {
    //     var senderId = HttpContext.Session.GetString("UserId");
    //     var SenderUser = await _context.Users.FindAsync(senderId);
    //     var senderName = User.Identity.Name; // Assuming you have user names stored here
    //
    //     if (string.IsNullOrEmpty(senderId))
    //     {
    //         return RedirectToAction("Login", "Account"); // Redirect to login if session is missing
    //     }
    //
    //     var model = new Chat
    //     {
    //         SenderID = SenderUser.Id,
    //         SenderName = SenderUser.UserName,
    //         ReceiverId = receiverId,
    //         ReceiverName = receiverName
    //     };
    //
    //     return View(model);
    // }


    public async Task<IActionResult> ChatWindowHelperToGetR (int receiverId)
    {
        var receiverUser0 = await _context.Teacher.FindAsync(receiverId);
        var receiverUser = await _context.Users.Where(x => x.Email == receiverUser0.Email).FirstOrDefaultAsync();
        return RedirectToAction("ChatWindow", "Chat", new { receiverId = receiverUser.Id });

    }



    [HttpGet]
    public async Task<IActionResult> ChatWindow(string receiverId)
    {
        var senderId = HttpContext.Session.GetString("UserId");
       // int result = int.Parse(receiverId);
        if (string.IsNullOrEmpty(senderId) || string.IsNullOrEmpty(receiverId))
        {
            return RedirectToAction("Login");
        }

        // Fetch the sender and receiver user details
        var senderUser = await _context.Users.FindAsync(senderId);
        // var receiverUser0 = await _context.Teacher.FindAsync(result);
        var receiverUser = await _context.Users.FindAsync(receiverId);

        // Fetch the chat history between the sender and receiver
        var chatHistory = await _context.Chats
            .Where(c => (c.SenderID == senderId && c.ReceiverId == receiverId) ||
                        (c.SenderID == receiverId && c.ReceiverId == senderId))
            .OrderBy(c => c.DateTime)
            .ToListAsync();

        // Create a new Chat object for the current chat window (with no initial message)
        var chat = new Chat();

        chat.SenderID = senderUser.Id;
        chat.SenderName = senderUser.FirstName;
        chat.ReceiverId = receiverUser.Id;
        chat.ReceiverName = receiverUser.FirstName;
        chat.Message = "";
        chat.DateTime = DateTime.Now;
        chat.IsRead = false;
      
        // Pass the chat history and chat object to the view
        var chatViewModel = new ChatViewModel
        {
            ChatHistory = chatHistory,
            CurrentChat = chat
        };

        return View(chatViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage(string senderId, string receiverId, string message)
    {
        var senderUser = await _context.Users.FindAsync(senderId);
        var receiverUser = await _context.Users.FindAsync(receiverId);
        if (string.IsNullOrWhiteSpace(senderId) || string.IsNullOrWhiteSpace(receiverId) || string.IsNullOrWhiteSpace(message))
        {
            TempData["ErrorMessage"] = "Invalid message data.";
            return RedirectToAction("ChatWindow", new { receiverId });
        }

        var chat = new Chat
        {
            SenderID = senderId,
            SenderName= senderUser.FirstName,
            ReceiverId = receiverId,
            ReceiverName = receiverUser.FirstName,
            Message = message,
            DateTime = DateTime.Now,
            IsRead = false
        };

        _context.Chats.Add(chat);
        await _context.SaveChangesAsync();

        return RedirectToAction("ChatWindow", new { receiverId });
    }






}
