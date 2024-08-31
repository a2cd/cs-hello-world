using System.Security.Cryptography;
using System.Text;

namespace cs_hello_world.Util;

public static class AesUtil
{
    public static readonly string AesKey = Environment.GetEnvironmentVariable("CS_HELLO_WORLD_AES_KEY") ?? "";

    /// <summary>
    /// AES解密
    /// </summary>
    /// <param name="text">密文字节数组</param>
    /// <param name="key">密钥（32位）</param>
    /// <returns>返回解密后的字符串</returns>
    public static string Decrypt(string text, string key)
    {
        var inputBytes = Convert.FromBase64String(text);
        var keyBytes = Encoding.UTF8.GetBytes(key);
        var ivBytes = Encoding.UTF8.GetBytes(key[..16]);
        using var aesAlg = Aes.Create();
        aesAlg.Padding = PaddingMode.PKCS7;
        aesAlg.Mode = CipherMode.CBC;
        aesAlg.Key = keyBytes;
        aesAlg.IV = ivBytes;
        var decrypt = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
        using var msEncrypt = new MemoryStream(inputBytes);
        using var csEncrypt = new CryptoStream(msEncrypt, decrypt, CryptoStreamMode.Read);
        using var srEncrypt = new StreamReader(csEncrypt);
        return srEncrypt.ReadToEnd();
    }


    /// <summary>
    /// AES加密算法
    /// </summary>
    /// <param name="text">明文字符串</param>
    /// <param name="key">密钥（32位）</param>
    /// <returns>字符串</returns>
    public static string Encrypt(string text, string key)
    {
        var keyBytes = Encoding.UTF8.GetBytes(key);
        var ivBytes = Encoding.UTF8.GetBytes(key[..16]);
        using var aesAlg = Aes.Create();
        aesAlg.Padding = PaddingMode.PKCS7;
        aesAlg.Mode = CipherMode.CBC;
        aesAlg.Key = keyBytes;
        aesAlg.IV = ivBytes;
        var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
        using var msEncrypt = new MemoryStream();
        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        {
            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(text);
            }
        }

        return Convert.ToBase64String(msEncrypt.ToArray());
    }
}
