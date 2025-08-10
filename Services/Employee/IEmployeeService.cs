using TimeTrackerClient.Services.Base;

namespace TimeTrackerClient.Services.Employee
{
    public interface IEmployeeService
    {
        Task<ApiResponse<IEnumerable<EmployeeReadDto>>> GetAllEmployeesAsync();
        Task<ApiResponse<EmployeeReadDto>> GetEmployeeByIdAsync(int id);
        Task<ApiResponse<EmployeeReadDto>> CreateEmployeeAsync(EmployeeCreateDto dto);
        Task<ApiResponse<bool>> UpdateEmployeeAsync(int id, EmployeeUpdateDto dto);
        Task<ApiResponse<bool>> DeleteEmployeeAsync(int id);
        Task<ApiResponse> RegisterEmployeeAsync(RegisterEmployeeDto dto);


    }

}
