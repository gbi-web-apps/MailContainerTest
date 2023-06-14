using MailContainerTest.Abstractions;
using Microsoft.Extensions.Logging;

namespace MailContainerTest.Adapters;

public sealed class LoggerAdapter<T> : ILoggerAdapter<T> where T : class
{
    private readonly ILogger<T> _logger;

    public LoggerAdapter(ILogger<T> logger)
    {
        _logger = logger;
    }

    public void LogError(Exception ex, string message)
    {
        _logger.LogError(message, ex);
    }
}