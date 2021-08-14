using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;

namespace CanedoLab.MS.Identity.API.Controllers
{
    [ApiController]
    public abstract class ApiController : Controller
    {
        protected ICollection<string> Errors = new Collection<string>();

        public bool HasErrors => Errors.Any();

        protected ActionResult CreateResponse([Optional] object result) 
        {
            if (HasErrors) 
            {
                return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>() 
                {
                    { "Validations", Errors.ToArray() }
                }));
            }

            return Ok(result);
        }

        protected ActionResult CreateResponse(ModelStateDictionary modelState) 
        {
            var errors = modelState.Values.SelectMany(v => v.Errors);

            foreach (var error in errors)
            {
                AddError(error.ErrorMessage);
            }

            return CreateResponse();
        }

        protected ICollection<string> AddError(string errorMessage) 
        {
            Errors.Add(errorMessage);

            return Errors;
        }

        protected void ClearErrors() => Errors.Clear();
    }
}
