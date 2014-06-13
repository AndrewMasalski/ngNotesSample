using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace NotesServiceApp.Controllers
{
    public class ArrayInputAttribute : ActionFilterAttribute
    {
        private readonly string _parameterName;

        public ArrayInputAttribute(string parameterName)
        {
            _parameterName = parameterName;
            Separator = ',';
        }

        public char Separator { get; set; }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ActionArguments.ContainsKey(_parameterName))
            {
                string parameters = string.Empty;
                if (actionContext.ControllerContext.RouteData.Values.ContainsKey(_parameterName))
                    parameters = (string)actionContext.ControllerContext.RouteData.Values[_parameterName];
                else if (actionContext.ControllerContext.Request.RequestUri.ParseQueryString()[_parameterName] != null)
                    parameters = actionContext.ControllerContext.Request.RequestUri.ParseQueryString()[_parameterName];

                actionContext.ActionArguments[_parameterName] = parameters.Split(Separator).Select(int.Parse).ToArray();
            }
        }

    }
}