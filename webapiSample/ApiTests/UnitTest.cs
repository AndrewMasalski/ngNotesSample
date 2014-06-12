using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotesServiceApp.Controllers;
using NotesServiceApp.Models;

namespace ApiTests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestValidateNote()
        {
            var note = new Note();
            var validationResults = note.Validate(new ValidationContext(note)).ToList();
            Assert.AreEqual(2, validationResults.Count);
            Assert.AreEqual("Id cannot be 0", validationResults[0].ErrorMessage);
            Assert.AreEqual("Title cannot be null or empty string", validationResults[1].ErrorMessage);
        }
        [TestMethod]
        public void TestGlogErrorResult()
        {
            var note = new Note();
            var glogErrorResult = new GlogErrorsResult(note);
            var httpResponseMessage = glogErrorResult.ExecuteAsync(CancellationToken.None).Result;
            var result = httpResponseMessage.Content.ReadAsStringAsync().Result;
            Assert.AreEqual("Id cannot be 0\r\nTitle cannot be null or empty string", result);
        }
    }
}
