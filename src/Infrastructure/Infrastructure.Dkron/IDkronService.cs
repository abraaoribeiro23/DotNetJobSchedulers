using Infrastructure.Dkron.Contracts;

namespace Infrastructure.Dkron;

public interface IDkronService
{
    Task<JobResponseDto?> GetJobByName(string jobName);
    Task<JobResponseDto?> CreateJob(JobPayloadDto dto);
    Task DeleteJobByName(string jobName);
}