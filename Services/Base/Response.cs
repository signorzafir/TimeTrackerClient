namespace TimeTrackerClient.Services.Base
{
    public class ApiResponse
    {
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; } = false;
        public string ValidationErrors { get; set; } = string.Empty;
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T Data { get; set; } = default!;
    }
}
