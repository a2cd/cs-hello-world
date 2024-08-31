using cs_hello_world.Config;
using cs_hello_world.Const;
using cs_hello_world.Tasks;
using cs_hello_world.Util;
using CSRedis;

namespace cs_hello_world.Component;
/// <summary>
/// LongRunning类似于Main线程，可以调用异步并使用.Wait()等待
/// </summary>
public static class BlockingListListener
{
    public static void Listen()
    {
        // 初始化redisClient
        var connStr =
            $"{Cfg.Redis.Host}:{Cfg.Redis.Port},password={EncUtil.Parse(Cfg.Redis.Password)},defaultDatabase=0,poolsize=5,idleTimeout=30000,connectTimeout=60000,syncTimeout=60000";
        var redisClient = new CSRedisClient(connStr);
        RedisHelper.Initialization(redisClient);

        Task.Factory.StartNew(ListenSfcsMoList, TaskCreationOptions.LongRunning);
        Task.Factory.StartNew(ListenSfcsUsnList, TaskCreationOptions.LongRunning);
    }

    private static readonly TimeSpan ExpiryTime = TimeSpan.FromSeconds(60);
    private static readonly TimeSpan WaitTime = TimeSpan.FromSeconds(60);

    private static void ListenSfcsMoList()
    {
        while (true)
        {
            try
            {
                // expiryTime 锁过期时间，如果rLock对象存活会有watchdog续命
                // waitTime 等待时间，没获取到锁sleep的时间
                // retryTime 重试时间，等待时间内多久重试一次
                using var rLock = RedisUtil.RedLockFactory.CreateLock(RedisKey.SfcsMoList, ExpiryTime,
                    WaitTime, TimeSpan.Zero);
                if (!rLock.IsAcquired) continue;
                var data = RedisHelper.BRPop(50, RedisKey.SfcsMoList);
                if (data == null) continue;
                Console.WriteLine($"{DateTime.Now} MO data received: {data}");
            }
            catch (Exception e)
            {
                // for redis not running
                Thread.Sleep(30000);
            }
        }
        // ReSharper disable once FunctionNeverReturns
    }

    private static void ListenSfcsUsnList()
    {
        while (true)
        {
            try
            {
                using var rLock = RedisUtil.RedLockFactory.CreateLock(RedisKey.SfcsUsnListLocal, ExpiryTime,
                    WaitTime, TimeSpan.Zero);
                if (!rLock.IsAcquired) continue;
                var data = RedisHelper.BRPop(50, RedisKey.SfcsUsnListLocal);
                if (data == null) continue;

                try
                {
                    Console.WriteLine($"1 : {Environment.CurrentManagedThreadId}");
                    SyncUsnTask.ExecuteAsync().GetAwaiter().GetResult();
                    Console.WriteLine($"4 : {Environment.CurrentManagedThreadId}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"execute error: {e.Message}");
                    continue;
                }

                Console.WriteLine($"{DateTime.Now} USN data received: {data}");
            }
            catch (Exception e)
            {
                // for redis not running
                Thread.Sleep(30000);
            }
        }
        // ReSharper disable once FunctionNeverReturns
    }
}
