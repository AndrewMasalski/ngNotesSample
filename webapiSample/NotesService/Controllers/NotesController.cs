using System;
using System.Web.Http.Cors;
using GlogWebService.Controllers;
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
                return db.Notes.Skip(pageNum*pageSize).Take(pageSize).ToList();
            }
        }

        public IHttpActionResult GetNote(int id)
        {
            using (var db = new FakeDbCtx())
            {
                var note = db.Notes.FirstOrDefault(p => p.Id == id);
                if (note == null) return NotFound();
                return Ok(note);
            }
        }

        [ArrayInput("id", Separator = ';')]
        public IHttpActionResult GetNotes(int[] ids)
        {
            using (var db = new FakeDbCtx())
            {
                var note = db.Notes.FirstOrDefault(p => ids.Contains(p.Id));
                if (note == null) return NotFound();
                return Ok(note);
            }
        }

        public IHttpActionResult Add(Note note)
        {
            return Ok(note);
        }

        public IHttpActionResult PutNote(int id, Note note)
        {
            using (var db = new FakeDbCtx())
            {
                var found = db.Notes.FirstOrDefault(n => n.Id == id);
                if (found == null) return NotFound();
                var errors = new List<string>();
                if (note.Id < 0)
                    errors.Add("Id cannot be less than 0");
                if (string.IsNullOrEmpty(note.Title))
                    errors.Add("Title cannot be empty");
                if (errors.Any())
                    return new GlogErrorsResult(errors);
                // set new values to Entity
                found.Title = note.Title;
                found.Description = note.Description;

                // send updated Entity to DB
//                db.Entry(course).State = EntityState.Modified;
//                db.SaveChanges();

                return Ok(found);
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
