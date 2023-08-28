using System.Net;
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

    public async Task<JobResponseDto?> GetJobByName(string jobName)
    {
        try
        {
            var response = await _httpClient.GetAsync($"v1/jobs/{jobName}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<JobResponseDto>();
        }
        catch (HttpRequestException e) when (e.StatusCode==HttpStatusCode.NotFound)
        {
            return null;
        }
        catch (Exception e)
        {
            throw new DkronServiceException(e);
        }
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

    public async Task DeleteJobByName(string jobName)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"v1/jobs/{jobName}");
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            throw new DkronServiceException(e);
        }
    }
}