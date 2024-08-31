using cs_hello_world.Const;
using cs_hello_world.Util;

namespace cs_hello_world.Tasks;

public static class SyncUsnTask
{
    public static async Task EnqueueAsync()
    {
        await RedisUtil.Db.ListLeftPushAsync(RedisKey.SfcsUsnListLocal, "sync_sfcs_usn");
    }

    public static async Task ExecuteAsync()
    {
        Console.WriteLine($"2 开始执行 : {Environment.CurrentManagedThreadId}");
        await Task.Delay(5000);
        Console.WriteLine($"3 执行完成 : {Environment.CurrentManagedThreadId}");
    }

}
