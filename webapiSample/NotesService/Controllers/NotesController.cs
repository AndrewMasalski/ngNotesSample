using System.Web.Http.Cors;
using NotesServiceApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace NotesServiceApp.Controllers
{
    [EnableCors("*", "*", "*")]
    public class NotesController : ApiController
    {
        private readonly Note[] notes =
        { 
            new Note { Id = 1, Title = "Kill all humans", Description = "With deadly virus or something..." }, 
            new Note { Id = 2, Title = "Ressurect all humans", Description = "Virus will do ok" }, 
        };

        public IEnumerable<Note> GetAllNotes()
        {
            return notes;
        }

        public IHttpActionResult GetNotes(int id)
        {
            var note = notes.FirstOrDefault(p => p.Id == id);
            if (note == null)
            {
                return NotFound();
            }
            return Ok(note);
        }
    }
}
