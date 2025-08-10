using TimeTrackerClient.Services.Base;

namespace TimeTrackerClient.Services.WorkEntry
{
    public interface IWorkEntryService
    {
        Task<ApiResponse<IEnumerable<WorkEntryReadDto>>> GetByEmployeeAsync(int employeeId);
        Task<ApiResponse<WorkEntryReadDto>> CreateAsync(WorkEntryCreateDto dto);
        Task<ApiResponse<WorkEntryReadDto>> GetByIdAsync(int id);
        Task<ApiResponse<bool>> UpdateAsync(int id, WorkEntryUpdateDto dto);
        Task<ApiResponse<bool>> DeleteAsync(int id);
    }
}
