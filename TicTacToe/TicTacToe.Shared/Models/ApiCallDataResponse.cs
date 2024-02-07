namespace TicTacToe.Api.Shared.Models
{
    public class ApiCallDataResponse<TDto> : ApiCallResponse
    {
        public TDto Dto { get; set; }
    }
}
    