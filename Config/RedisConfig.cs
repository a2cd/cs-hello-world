namespace cs_hello_world.Config;

public class RedisConfig
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public int? Database { get; set; }
}
