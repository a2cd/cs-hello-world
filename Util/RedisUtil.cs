using cs_hello_world.Config;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using StackExchange.Redis;

namespace cs_hello_world.Util;

public static class RedisUtil
{
    public static ConnectionMultiplexer Conn =
        ConnectionMultiplexer.Connect(
            $"{Cfg.Redis.Host}:{Cfg.Redis.Port},password={Cfg.Redis.Password},ConnectTimeout=10000");

    public static readonly IDatabase Db = Conn.GetDatabase();

    public static readonly RedLockFactory RedLockFactory =
        RedLockFactory.Create(new List<RedLockMultiplexer> { Conn });

}
