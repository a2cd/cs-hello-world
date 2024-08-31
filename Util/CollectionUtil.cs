using StringSorterLib.Core;

namespace cs_hello_world.Util;

/// <summary>
/// 集合工具类
/// </summary>
public static class CollectionUtil
{
    public static bool IsEmpty<T>(IEnumerable<T>? input)
    {
        if (input == null) return true;
        return !input.Any();
    }

    public static List<List<T>> Partition<T>(List<T> list, int batchSize)
    {
        var chunks = new List<List<T>>();
        if (batchSize <= 0)
            return chunks;
        for (var i = 0; i < list.Count; i += batchSize)
        {
            var chunkEnd = Math.Min(i + batchSize, list.Count);
            var chunk = new List<T>(list.GetRange(i, chunkEnd - i));
            chunks.Add(chunk);
        }

        return chunks;
    }

    /// <summary>
    /// 字符串升序排序
    /// </summary>
    /// <param name="list"></param>
    /// <param name="orderType"></param>
    /// <returns></returns>
    public static List<string> StrSort(List<string> list, OrderType orderType = OrderType.Asc)
    {
        return StringSorter.Sort(list, orderType);
    }
}
