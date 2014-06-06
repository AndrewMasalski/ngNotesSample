using System;
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
        public IEnumerable<Note> GetAllNotes(int pageNum = 0, int pageSize = 25)
        {
            using (var db = new FakeDbCtx())
            {
                return db.Notes.Skip(pageNum*pageSize).Take(25).ToList();
            }
        }

        public IHttpActionResult GetNote(int id)
        {
            using (var db = new FakeDbCtx())
            {
                var note = db.Notes.FirstOrDefault(p => p.Id == id);
                if (note == null)
                {
                    return NotFound();
                }
                return Ok(note);
            }
        }
    }

    public class FakeDbCtx : IDisposable
    {
        public FakeDbCtx()
        {
            Notes = new List<Note>();
            for (var i = 0; i < 10000; i++)
                Notes.Add(new Note() { Id = i, Title = "Note" + i });
        }

        public List<Note> Notes { get; private set; }

        public void Dispose()
        {
            Notes.Clear();
        }
    }


}
