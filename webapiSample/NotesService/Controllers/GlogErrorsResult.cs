using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace GlogWebService.Controllers
{
    public class GlogErrorsResult : IHttpActionResult
    {
        private readonly List<string> errors;

        public GlogErrorsResult(List<string> errors)
        {
            this.errors = errors;
        }

        public GlogErrorsResult(string message)
        {
            errors = new List<string> { message };
        }

        public GlogErrorsResult(IValidatableObject validatable)
        {
            var validationResults = validatable.Validate(new ValidationContext(validatable));
            errors = validationResults.Select(vr => vr.ErrorMessage).ToList();
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            response.Content = new StringContent(string.Join(Environment.NewLine, errors));
            return Task.FromResult(response);
        }
    }
}