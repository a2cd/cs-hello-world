namespace cs_hello_world.Config;

public static class Cfg
{
    public static RedisConfig Redis { get; private set; } = null!;
    public static LoggingConfig Logging { get; private set; } = null!;
    public static AppInfoConfig AppInfo { get; private set; } = null!;

    public static void Init(IConfiguration configuration)
    {
        var redisConfig = new RedisConfig();
        configuration.Bind("Redis", redisConfig);
        Redis = redisConfig;

        var loggingConfig = new LoggingConfig();
        configuration.Bind("Logging", loggingConfig);
        Logging = loggingConfig;

        var appInfoConfig = new AppInfoConfig();
        configuration.Bind("AppInfo", appInfoConfig);
        AppInfo = appInfoConfig;
    }
}
