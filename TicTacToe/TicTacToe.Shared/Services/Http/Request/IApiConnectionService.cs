using RestSharp;
using System;
using System.Threading.Tasks;
using TicTacToe.Api.Shared.Models;

namespace TicTacToe.Api.Shared.Services.Http.Request
{
    public interface IApiConnectionService
    {
        Task<ApiCallResponse> MakeRequestAsync(string resource, Method httpMethod);

        Task<ApiCallResponse> MakeRequestAsync<TRequestDto>(string resource, Method httpMethod, TRequestDto argument);

        Task<ApiCallDataResponse<TResponseDto>> MakeRequestAsync<TResponseDto>(string resource, Method httpMethod);

        Task<ApiCallDataResponse<TResponseDto>> MakeRequestAsync<TRequestDto, TResponseDto>(string resource, Method httpMethod, TRequestDto argument);
    }
}
