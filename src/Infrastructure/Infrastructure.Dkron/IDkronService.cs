using Infrastructure.Dkron.Contracts.Base;

namespace Infrastructure.Dkron;

public interface IDkronService
{
    Task<DkronJobResponse?> GetJobByName(string jobName);
    Task<DkronJobResponse?> CreateJob<T>(T dto) where T : DkronJobPayload;
    Task DeleteJobByName(string jobName);
}