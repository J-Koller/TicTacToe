using System.Net;

namespace TicTacToe.Api.Shared.Models
{
    public class ApiCallResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public string ErrorMessage { get; set; }

        public bool ResponseSuccessful => ErrorMessage is null;

        public string FullError => $"{StatusCode} - {ErrorMessage ?? "unknown"}";
    }
}
