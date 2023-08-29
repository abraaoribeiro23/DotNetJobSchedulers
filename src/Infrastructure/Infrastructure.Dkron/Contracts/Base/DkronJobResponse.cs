using System.Text.Json.Serialization;

namespace Infrastructure.Dkron.Contracts.Base;

public class DkronJobResponse
{
    public string? Name { get; set; }
    [JsonPropertyName("display_name")]
    public string? DisplayName { get; set; }
    public string? Schedule { get; set; }
    public string? Timezone { get; set; }
    public string? Owner { get; set; }
    [JsonPropertyName("owner_email")]
    public string? OwnerEmail { get; set; }
    [JsonPropertyName("success_count")]
    public int SuccessCount { get; set; }
    [JsonPropertyName("error_count")]
    public int ErrorCount { get; set; }
    [JsonPropertyName("last_success")]
    public DateTime? LastSuccess { get; set; }
    [JsonPropertyName("last_error")]
    public DateTime? LastError { get; set; }
    public bool Disabled { get; set; }
    public Dictionary<string, string> Tags { get; set; } = new();
    public Dictionary<string, string> Metadata { get; set; } = new();
    public int Retries { get; set; }
    [JsonPropertyName("parent_job")]
    public string? ParentJob { get; set; }
    [JsonPropertyName("dependent_jobs")]
    public List<string> DependentJobs { get; set; } = new();
    public Dictionary<string, object> Processors { get; set; } = new();
    public string? Concurrency { get; set; }
    public string? Executor { get; set; }
    [JsonPropertyName("executor_config")]
    public Dictionary<string, string> ExecutorConfig { get; set; } = new();
    public string? Status { get; set; }
    public DateTime Next { get; set; }
}