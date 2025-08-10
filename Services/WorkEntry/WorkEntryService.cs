using Blazored.LocalStorage;
using TimeTrackerClient.Services.Base;

namespace TimeTrackerClient.Services.WorkEntry
{
    public class WorkEntryService: BaseHttpService, IWorkEntryService
    {
        private readonly IClient client;

        public WorkEntryService(ILocalStorageService localStorage, IClient client)
            : base(localStorage, client)
        {
            this.client = client;
        }

        public async Task<ApiResponse<IEnumerable<WorkEntryReadDto>>> GetByEmployeeAsync(int employeeId)
        {
            try
            {
                await GetBearerToken();
                var entries = await client.GetWorkEntrybyEmployeeIdAsync(employeeId);

                return new ApiResponse<IEnumerable<WorkEntryReadDto>>
                {
                    Success = true,
                    Data = entries
                };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<IEnumerable<WorkEntryReadDto>>(ex);
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<WorkEntryReadDto>>
                {
                    Success = false,
                    Message = "An unexpected error occurred while fetching work entries"
                };
            }
        }

        public async Task<ApiResponse<WorkEntryReadDto>> CreateAsync(WorkEntryCreateDto dto)
        {
            try
            {
                await GetBearerToken();
                var createdEntry = await client.WorkEntryPOSTAsync(dto);

                return new ApiResponse<WorkEntryReadDto>
                {
                    Success = true,
                    Data = createdEntry,
                    Message = "Work entry created successfully"
                };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<WorkEntryReadDto>(ex);
            }
            catch (Exception ex)
            {
                return new ApiResponse<WorkEntryReadDto>
                {
                    Success = false,
                    Message = "An unexpected error occurred while creating the work entry"
                };
            }
        }

        public async Task<ApiResponse<WorkEntryReadDto>> GetByIdAsync(int id)
        {
            try
            {
                await GetBearerToken();
                var entry = await client.WorkEntryGETAsync(id);

                return new ApiResponse<WorkEntryReadDto>
                {
                    Success = true,
                    Data = entry
                };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<WorkEntryReadDto>(ex);
            }
            catch (Exception ex)
            {
                return new ApiResponse<WorkEntryReadDto>
                {
                    Success = false,
                    Message = "An unexpected error occurred while fetching the work entry"
                };
            }
        }

        public async Task<ApiResponse<bool>> UpdateAsync(int id, WorkEntryUpdateDto dto)
        {
            try
            {
                await GetBearerToken();
                await client.WorkEntryPUTAsync(id, dto);

                return new ApiResponse<bool>
                {
                    Success = true,
                    Data = true,
                    Message = "Work entry updated successfully"
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
                    Message = "An unexpected error occurred while updating the work entry"
                };
            }
        }

        public async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            try
            {
                await GetBearerToken();
                await client.WorkEntryDELETEAsync(id);

                return new ApiResponse<bool>
                {
                    Success = true,
                    Data = true,
                    Message = "Work entry deleted successfully"
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
                    Message = "An unexpected error occurred while deleting the work entry"
                };
            }
        }
    }
}
