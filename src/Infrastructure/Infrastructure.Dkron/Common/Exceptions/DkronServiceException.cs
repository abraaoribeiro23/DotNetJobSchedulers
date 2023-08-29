namespace Infrastructure.Dkron.Common.Exceptions;

public class DkronServiceException : Exception
{
    public DkronServiceException(Exception ex)
        : base("Error executing DkronService.", ex)
    {
    }
    public DkronServiceException(string msg, Exception? ex = null)
        : base(msg, ex)
    {
    }
}