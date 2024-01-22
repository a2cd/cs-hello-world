namespace cs_hello_world.Config;

public static class Cfg
{
    public static RedisConfig Redis { get; private set; } = null!;

    public static void Init(IConfiguration configuration)
    {
        var redisConfig = new RedisConfig();
        configuration.Bind("Redis", redisConfig);
        Redis = redisConfig;
    }
}
