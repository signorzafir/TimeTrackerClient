using Blazored.LocalStorage;
using TimeTrackerClient.Services.Base;

namespace TimeTrackerClient.Services.Employee
{

    public class EmployeeService : BaseHttpService, IEmployeeService
    {
        private readonly IClient client;

        public EmployeeService(ILocalStorageService localStorage, IClient client)
            : base(localStorage, client)
        {
            this.client = client;
        }

        public async Task<ApiResponse<IEnumerable<EmployeeReadDto>>> GetAllEmployeesAsync()
        {
            try
            {
                await GetBearerToken();
                var employees = await client.EmployeeAllAsync();

                return new ApiResponse<IEnumerable<EmployeeReadDto>>
                {
                    Success = true,
                    Data = employees
                };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<IEnumerable<EmployeeReadDto>>(ex);
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<EmployeeReadDto>>
                {
                    Success = false,
                    Message = "An unexpected error occurred while fetching employees",
                    ValidationErrors = ex.Message
                };
            }
        }

        public async Task<ApiResponse<EmployeeReadDto>> GetEmployeeByIdAsync(int id)
        {
            try
            {
                await GetBearerToken();
                var employee = await client.EmployeeGETAsync(id);

                return new ApiResponse<EmployeeReadDto>
                {
                    Success = true,
                    Data = employee
                };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<EmployeeReadDto>(ex);
            }
            catch (Exception ex)
            {
                return new ApiResponse<EmployeeReadDto>
                {
                    Success = false,
                    Message = "An unexpected error occurred while fetching the employee",
                    ValidationErrors = ex.Message
                };
            }
        }

        public async Task<ApiResponse<EmployeeReadDto>> CreateEmployeeAsync(EmployeeCreateDto dto)
        {
            try
            {
                await GetBearerToken();
                var createdEmployee = await client.EmployeePOSTAsync(dto);

                return new ApiResponse<EmployeeReadDto>
                {
                    Success = true,
                    Data = createdEmployee,
                    Message = "Employee created successfully"
                };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<EmployeeReadDto>(ex);
            }
            catch (Exception ex)
            {
                return new ApiResponse<EmployeeReadDto>
                {
                    Success = false,
                    Message = "An unexpected error occurred while creating the employee",
                    ValidationErrors = ex.Message
                };
            }
        }

        public async Task<ApiResponse<bool>> UpdateEmployeeAsync(int id, EmployeeUpdateDto dto)
        {
            try
            {
                await GetBearerToken();
                await client.EmployeePUTAsync(id, dto);

                return new ApiResponse<bool>
                {
                    Success = true,
                    Data = true,
                    Message = "Employee updated successfully"
                };
            }
            //catch (ApiException ex)
            //{
            //    return ConvertApiExceptions<bool>(ex);
            //}
            catch (Exception ex)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "An unexpected error occurred while updating the employee",
                    ValidationErrors = ex.Message
                };
            }
        }

        public async Task<ApiResponse<bool>> DeleteEmployeeAsync(int id)
        {
            try
            {
                await GetBearerToken();
                await client.EmployeeDELETEAsync(id);

                return new ApiResponse<bool>
                {
                    Success = true,
                    Data = true,
                    Message = "Employee deleted successfully"
                };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<bool>(ex);
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "An unexpected error occurred while deleting the employee",
                    ValidationErrors = ex.Message
                };
            }
        }

        public async Task<ApiResponse> RegisterEmployeeAsync(RegisterEmployeeDto dto)
        {
            try
            {
                await GetBearerToken();
                await client.RegisterEmployeeAsync(dto);

                return new ApiResponse
                {
                    Success = true,
                    Message = "Employee Registered successfully"
                };
            }
            //catch (ApiException ex)
            //{
            //    return ConvertApiExceptions<bool>(ex);
            //}
            catch (Exception ex)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "An unexpected error occurred while creating the employee",
                    ValidationErrors = ex.Message
                };
            }
        }

        //private ApiResponse<T> ConvertApiException<T>(ApiException apiException)
        //{
        //    return new ApiResponse<T>
        //    {
        //        Success = false,
        //        Message = apiException.StatusCode switch
        //        {
        //            404 => "Employee not found",
        //            401 => "Unauthorized access",
        //            403 => "Access forbidden",
        //            500 => "Internal server error occurred",
        //            _ => $"API error: {apiException.StatusCode}"
        //        },
        //        ValidationErrors = apiException.Response ?? apiException.Message
        //    };
        //}
    }
}

