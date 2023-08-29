using System.Text.Json.Serialization;

namespace Infrastructure.Dkron.Contracts.Base;

public abstract class DkronJobPayload
{
    public string? Name { get; set; }
    [JsonPropertyName("display_name")]
    public string? DisplayName { get; set; }
    public string? Schedule { get; set; }
    public string? Timezone { get; set; }
    public string? Owner { get; set; }
    [JsonPropertyName("owner_email")]
    public string? OwnerEmail { get; set; }
    public bool Disabled { get; set; }
    public Dictionary<string, string> Tags { get; set; } = new();
    public Dictionary<string, string> Metadata { get; set; } = new();
    public int Retries { get; set; }
    [JsonPropertyName("parent_job")]
    public string? ParentJob { get; set; }
    public Dictionary<string, object> Processors { get; set; } = new();
    public string Concurrency => "allow";
}