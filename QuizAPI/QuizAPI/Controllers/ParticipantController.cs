//IActionResult — This is an interface (a contract that classes can follow).
//ActionResult<T> — This is a class that can hold either a result or a value of type T.
//IEnumerable<T> — This is an interface for collections you can loop through (like lists).

//IActionResult: Any HTTP response, no type info.
//ActionResult<T>: HTTP response with typed data T.
//ActionResult<IEnumerable<T>>: HTTP response with a typed list of T

#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizAPI.Models;

namespace QuizAPI.Controllers
{
    //Defines the route URL template for the controller. [controller] placeholder will be replaced by the controller name(i.e., "Participant").
    //So all routes start with /api/Participant.
    [Route("api/[controller]")]
    //Marks this as an API controller. Enables features like automatic model validation and binding behavior
    [ApiController]
    //inherits from ControllerBase which provides basic API controller
    public class ParticipantController : ControllerBase
    {
        //Private readonly field to hold the database context instance, which manages database operation
        private readonly QuizDbContext _context;

        //Constructor receives a QuizDbContext instance through dependency injection and assigns it to the private field.

        public ParticipantController(QuizDbContext context)
        {
            _context = context;
        }

        // GET: api/Participant
        [HttpGet]
        //method is asynchronous (using async/await) to avoid blocking the server while fetching data.
        //ToListAsync() to get all records from the Participants table.
        //The result is wrapped inside ActionResult<IEnumerable<Participant>> which means it will send back an HTTP response containing a list of Participant objects.
        public async Task<ActionResult<IEnumerable<Participant>>> GetParticipants()
        {
            return await _context.Participants.ToListAsync();
        }

        // GET: api/Participant/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Participant>> GetParticipant(int id)
        {
            var participant = await _context.Participants.FindAsync(id);

            if (participant == null)
            {
                return NotFound();
            }

            return participant;
        }

        // PUT: api/Participant/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParticipant(int id, ParticipantRestult _participantResult)
        {
            if (id != _participantResult.ParticipantId)
            {
                return BadRequest();
            }

            // get all current details of the record, then update with quiz results
            Participant participant = _context.Participants.Find(id);
            participant.Score = _participantResult.Score;
            participant.TimeTaken = _participantResult.TimeTaken;

            _context.Entry(participant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipantExists(id))
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

        // POST: api/Participant
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Participant>> PostParticipant(Participant participant)
        {
            var temp = _context.Participants
                .Where(x => x.Name == participant.Name
                && x.Email == participant.Email)
                .FirstOrDefault();

            if (temp == null)
            {
                _context.Participants.Add(participant);
                await _context.SaveChangesAsync();
            }
            else
                participant = temp;

            return Ok(participant);
        }

        // DELETE: api/Participant/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipant(int id)
        {
            var participant = await _context.Participants.FindAsync(id);
            if (participant == null)
            {
                return NotFound();
            }

            _context.Participants.Remove(participant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParticipantExists(int id)
        {
            return _context.Participants.Any(e => e.ParticipantId == id);
        }
    }
}