namespace cs_hello_world.Util;

public static class EncUtil
{
    /// <summary>
    /// 解析ENC()
    /// </summary>
    /// <param name="text">密文</param>
    /// <returns>返回解密后的字符串</returns>
    public static string Parse(string text)
    {
        if (!text.StartsWith("ENC(") || !text.EndsWith(')'))
            throw new Exception("密文必须以 'ENC(' 开头, 以 ')' 结尾");
        string str;
        try
        {
            // 去除开头 'ENC(' 和结尾 ')'
            str = AesUtil.Decrypt(text[4..^1], AesUtil.AesKey);
        }
        catch (Exception)
        {
            throw new Exception("ENC() 解析错误: " + text);
        }

        return str;
    }
}
