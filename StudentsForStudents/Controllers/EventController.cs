using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsForStudents.Context;
using StudentsForStudents.Models;

namespace StudentsForStudents.Controllers
{
    [Route("Event")]
    public class EventController : Controller
    {
        private readonly SFSDBContect _context;

        public EventController(SFSDBContect context)
        {
            _context = context;
        }

        [HttpGet("GetEvents")]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _context.Events.ToListAsync();
            return Ok(events);
        }

        [HttpPost("AddEvent")]
        public async Task<IActionResult> AddEvent([FromBody] Event newEvent)
        {
            if (newEvent == null || string.IsNullOrWhiteSpace(newEvent.Title))
                return BadRequest("Invalid event data.");

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();
            return Ok(newEvent);
        }

        [HttpPut("UpdateEvent/{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] Event updatedEvent)
        {
            var existingEvent = await _context.Events.FindAsync(id);
            if (existingEvent == null)
                return NotFound("Event not found.");

            existingEvent.Title = updatedEvent.Title;
            existingEvent.StartDate = updatedEvent.StartDate;

            await _context.SaveChangesAsync();
            return Ok(existingEvent);
        }

        [HttpDelete("DeleteEvent/{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var existingEvent = await _context.Events.FindAsync(id);
            if (existingEvent == null)
                return NotFound("Event not found.");

            _context.Events.Remove(existingEvent);
            await _context.SaveChangesAsync();
            return Ok();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
