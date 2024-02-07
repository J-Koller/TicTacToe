using RestSharp;
using System;
using System.Threading.Tasks;
using TicTacToe.Api.Shared.Models;

namespace TicTacToe.Api.Shared.Services.Http.Request
{
    public class RestSharpApiConnectionService : IApiConnectionService
    {
        private readonly RestClient _restClient;

        private readonly string _domain = "http://localhost:5159";

        private readonly string _friendlyErrorMessage = "Oops! Something went wrong. Lets try again.";

        public RestSharpApiConnectionService()
        {
            _restClient = new RestClient(_domain);
        }

        public async Task<ApiCallResponse> MakeRequestAsync(string resource, Method httpMethod)
        {
            var restRequest = new RestRequest(resource);

            var apiCallResponse = await InternalMakeRequestAsync(httpMethod, restRequest);

            return apiCallResponse;
        }

        public async Task<ApiCallResponse> MakeRequestAsync<TRequestDto>(string resource, Method httpMethod, TRequestDto argument)
        {
            var restRequest = new RestRequest(resource);

            restRequest.AddBody(argument);

            var apiCallResponse = await InternalMakeRequestAsync(httpMethod, restRequest);

            return apiCallResponse;
        }

        public async Task<ApiCallDataResponse<TResponseDto>> MakeRequestAsync<TResponseDto>(string resource, Method httpMethod)
        {
            var restRequest = new RestRequest(resource);

            var apiCallDataResponse = await InternalMakeRequestAsync<TResponseDto>(httpMethod, restRequest);

            return apiCallDataResponse;
        }

        public async Task<ApiCallDataResponse<TResponseDto>> MakeRequestAsync<TRequestDto, TResponseDto>(string resource, Method httpMethod, TRequestDto argument)
        {
            var restRequest = new RestRequest(resource);

            restRequest.AddBody(argument);

            var apiCallDataResponse = await InternalMakeRequestAsync<TResponseDto>(httpMethod, restRequest);

            return apiCallDataResponse;
        }

        private async Task<ApiCallResponse> InternalMakeRequestAsync(Method httpMethod, RestRequest restRequest)
        {
            try
            {
                var restResponse = await _restClient.ExecuteAsync(restRequest, httpMethod);

                var deserializedRestResponse = _restClient.Deserialize<ApiCallResponse>(restResponse);

                if (deserializedRestResponse.Data is null)
                {
                    var errorResponse = new ApiCallResponse
                    {
                        ErrorMessage = _friendlyErrorMessage,
                        StatusCode = deserializedRestResponse.StatusCode
                    };

                    return errorResponse;
                }

                var apiCallResponse = deserializedRestResponse.Data;
                apiCallResponse.StatusCode = deserializedRestResponse.StatusCode;

                return apiCallResponse;
            }
            catch (Exception)
            {
                var apiCallResponse = new ApiCallResponse
                {
                    ErrorMessage = _friendlyErrorMessage
                };

                return apiCallResponse;
            }
        }

        private async Task<ApiCallDataResponse<TResponseDto>> InternalMakeRequestAsync<TResponseDto>(Method httpMethod, RestRequest restRequest)
        {
            try
            {
                var restResponse = await _restClient.ExecuteAsync(restRequest, httpMethod);

                var deserializedRestResponse = _restClient.Deserialize<ApiCallDataResponse<TResponseDto>>(restResponse);

                if (deserializedRestResponse.Data is null)
                {
                    var errorResponse = new ApiCallDataResponse<TResponseDto>
                    {
                        ErrorMessage = _friendlyErrorMessage,
                        StatusCode = deserializedRestResponse.StatusCode
                    };

                    return errorResponse;
                }

                var apiCallDataResponse = deserializedRestResponse.Data;
                apiCallDataResponse.StatusCode = deserializedRestResponse.StatusCode;

                return apiCallDataResponse;
            }
            catch (Exception)
            {
                var apiCallDataResponse = new ApiCallDataResponse<TResponseDto>
                {
                    ErrorMessage = _friendlyErrorMessage
                };

                return apiCallDataResponse;
            }
        }
    }
}
