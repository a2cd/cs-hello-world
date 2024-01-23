namespace cs_hello_world.Config;

public class LoggingConfig
{
    public LoggingLogLevelConfig LogLevel { get; set; }
}

public class LoggingLogLevelConfig
{
    public string Default { get; set; }
}
