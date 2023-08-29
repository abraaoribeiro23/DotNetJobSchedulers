using System.Net;
using System.Net.Http.Json;
using Infrastructure.Dkron.Common.Exceptions;
using Infrastructure.Dkron.Contracts.Base;

namespace Infrastructure.Dkron;

public class DkronService : IDkronService
{
    private readonly HttpClient _httpClient;

    public DkronService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<DkronJobResponse?> GetJobByName(string jobName)
    {
        try
        {
            var response = await _httpClient.GetAsync($"v1/jobs/{jobName}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<DkronJobResponse>();
        }
        catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
        catch (Exception ex)
        {
            throw new DkronServiceException($"Error Getting Job Schedule {jobName}", ex);
        }
    }

    public async Task<DkronJobResponse?> CreateJob<T>(T dto) where T : DkronJobPayload
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("v1/jobs", dto);
            return await HandleCreateJobHttpResponse(response);
        }
        catch (Exception ex)
        {
            throw new DkronServiceException($"Error Creating Job Schedule {dto.Name}", ex);
        }
    }

    private static async Task<DkronJobResponse?> HandleCreateJobHttpResponse(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<DkronJobResponse>();
        }

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            var errorMsg = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(errorMsg);
        }

        response.EnsureSuccessStatusCode();

        return null;
    }

    public async Task DeleteJobByName(string jobName)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"v1/jobs/{jobName}");
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            throw new DkronServiceException($"Error Deleting Job Schedule {jobName}", ex);
        }
    }
}