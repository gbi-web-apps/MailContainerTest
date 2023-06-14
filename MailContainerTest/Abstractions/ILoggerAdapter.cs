namespace MailContainerTest.Abstractions;

public interface ILoggerAdapter<T>
{
    void LogError(Exception ex,string message);
}