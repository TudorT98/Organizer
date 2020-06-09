using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organizer.Model;

namespace Organizer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarsController : ControllerBase
    {
        private readonly OrganizerDbContext _context;

        public CalendarsController(OrganizerDbContext context)
        {
            _context = context;
        }

        //GET
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Calendar>>> GetBookingForUserById(Guid userId)
        {
            return await _context.Bookings.Where(b => b.UserId == userId).ToListAsync();
        }
        //GET
        [HttpGet("{username}")]
        public async Task<ActionResult<IEnumerable<Calendar>>> GetBookingForUserByUserName(string username)
        {
            return await _context.Bookings.Where(b => b.user.UserName == username).ToListAsync();
        }


        // GET: api/Calendars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Calendar>>> GetBookings()
        {
            return await _context.Bookings.ToListAsync();
        }

        // GET: api/Calendars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Calendar>> GetCalendar(Guid id)
        {
            var calendar = await _context.Bookings.FindAsync(id);

            if (calendar == null)
            {
                return NotFound();
            }

            return calendar;
        }

        // PUT: api/Calendars/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCalendar(Guid id, Calendar calendar)
        {
            if (id != calendar.Id)
            {
                return BadRequest();
            }

            _context.Entry(calendar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalendarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Calendars
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Calendar>> PostCalendar(Calendar calendar)
        {
            _context.Bookings.Add(calendar);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCalendar", new { id = calendar.Id }, calendar);
        }

        // DELETE: api/Calendars/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Calendar>> DeleteCalendar(Guid id)
        {
            var calendar = await _context.Bookings.FindAsync(id);
            if (calendar == null)
            {
                return NotFound();
            }

            _context.Bookings.Remove(calendar);
            await _context.SaveChangesAsync();

            return calendar;
        }

        private bool CalendarExists(Guid id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }
    }
}
