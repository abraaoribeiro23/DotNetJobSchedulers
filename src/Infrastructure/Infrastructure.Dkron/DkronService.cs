using System.Net.Http.Json;
using Infrastructure.Dkron.Common.Exceptions;
using Infrastructure.Dkron.Contracts;

namespace Infrastructure.Dkron;

public class DkronService : IDkronService
{
    private readonly HttpClient _httpClient;

    public DkronService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<JobResponseDto?> CreateJob(JobPayloadDto dto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("v1/jobs", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<JobResponseDto>();
        }
        catch (Exception e)
        {
            throw new DkronServiceException(e);
        }
    }

    public Task<bool> DoesJobExist(string jobName)
    {
        throw new NotImplementedException();
    }

    public Task DeleteJob(string jobName)
    {
        throw new NotImplementedException();
    }
}