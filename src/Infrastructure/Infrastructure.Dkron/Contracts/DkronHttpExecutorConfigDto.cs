namespace Infrastructure.Dkron.Contracts;

public class DkronHttpExecutorConfigDto
{
    public string Method { get; set; }
    public string Url { get; set; }
    public string Headers { get; set; }
    public string Body { get; set; }
    public string Timeout { get; set; }
    public string ExpectCode { get; set; }
    public string ExpectBody { get; set; }
    public string Debug { get; set; }
}