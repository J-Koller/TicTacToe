using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Net;
using TicTacToe.Api.Shared.Models;

namespace TicTacToe.Api.Controllers.Base
{
    [Route("[controller]")]
    public class CustomControllerBase : ControllerBase
    {
        private readonly string _databaseErrorMessage = "An error with the database has occured.";
        private readonly string _friendlyErrorMessage = "Oops! Something went wrong. Lets try again.";
        private readonly string _errorLogTemplate = "Error at: {@EndpointName}... ErrorMessage: {@ErrorMessage}";

        protected IActionResult InternalServerError(object error)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, error);
        }

        protected IActionResult HandleStandardExceptions(Exception e, string methodName, ApiCallResponse response)
        {
            Log.Error(_errorLogTemplate, methodName, e.Message);

            response.ErrorMessage = _friendlyErrorMessage;

            if (e is ArgumentNullException || e is ArgumentException || e is InvalidOperationException)
            {
                return BadRequest(response);
            }
            else if (e is DbUpdateException || e is SqlException)
            {
                response.ErrorMessage = _databaseErrorMessage;
            }

            return InternalServerError(response);
        }

        protected IActionResult HandleUniqueExceptions(Exception e, string methodName, ApiCallResponse response)
        {
            Log.Error(_errorLogTemplate, methodName, e.Message);

            response.ErrorMessage = _friendlyErrorMessage;
            
            if (e is ArgumentNullException)
            {
                return BadRequest(response);
            }
            if (e is ArgumentException || e is InvalidOperationException)
            {
                response.ErrorMessage = e.Message;

                return BadRequest(response);
            }
            else if (e is DbUpdateException || e is SqlException)
            {
                response.ErrorMessage = _databaseErrorMessage;
            }

            return InternalServerError(response);
        }
    }
}
