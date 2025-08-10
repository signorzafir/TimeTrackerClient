using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace TimeTrackerClient.Services.Base
{
    public class BaseHttpService
    {
        private readonly ILocalStorageService localStorage;
        private readonly IClient client;

        public BaseHttpService(ILocalStorageService localStorage, IClient client)
        {
            this.localStorage = localStorage;
            this.client = client;
        }
        //protected ApiResponse<T> ConvertApiException<T>(ApiException apiException)
        //{
        //    return new ApiResponse<T>
        //    {
        //        Success = false,
        //        Message = apiException.StatusCode switch
        //        {
        //            404 => "Resource not found",
        //            401 => "Unauthorized access",
        //            403 => "Access forbidden",
        //            500 => "Internal server error occurred",
        //            _ => $"API error: {apiException.StatusCode}"
        //        },
                
        //        ValidationErrors =  apiException.Response ?? apiException.Message
        //    };
        //}

        protected ApiResponse<T> ConvertApiExceptions<T>(ApiException apiException)
        {
            if (apiException.StatusCode == 400)
            {
                return new ApiResponse<T>() { Message = "Validation errors have occured.", ValidationErrors = apiException.Response, Success = false };
            }
            if (apiException.StatusCode == 404)
            {
                return new ApiResponse<T>() { Message = "Requested Item could not be found.", Success = false };
            }
            return new ApiResponse<T>() { Message = "Something went wrong, try again!", Success = false };

        }
        protected async Task GetBearerToken()
        {
            var token = await localStorage.GetItemAsync<string>("accessToken");
            if (token != null)
            {
                client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            }
        }
    }
}
