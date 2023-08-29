using System.Text.Json.Serialization;
using Infrastructure.Dkron.Contracts.Base;

namespace Infrastructure.Dkron.Contracts;

public class DkronHttpJobPayloadDto : DkronJobPayload
{
    public string Executor => "http";

    [JsonPropertyName("executor_config")]
    public DkronHttpExecutorConfigDto ExecutorConfig { get; set; } = new();
}