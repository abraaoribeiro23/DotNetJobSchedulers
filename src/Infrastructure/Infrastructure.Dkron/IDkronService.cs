using Infrastructure.Dkron.Contracts;

namespace Infrastructure.Dkron;

public interface IDkronService
{
    Task<JobResponseDto?> CreateJob(JobPayloadDto dto);
    Task<bool> DoesJobExist(string jobName);
    Task DeleteJob(string jobName);
}