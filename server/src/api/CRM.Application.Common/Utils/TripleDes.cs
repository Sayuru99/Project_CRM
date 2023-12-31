﻿using System.Security.Cryptography;
using System.Text;

namespace CRM.Application.Common;

public static class TripleDes
{
    public static string Encrypt(byte[] key, string info)
    {
        var des = CreateTripleDES(key);
        var ct = des.CreateEncryptor();
        var input = Encoding.UTF8.GetBytes(info);
        var output = ct.TransformFinalBlock(input, 0, input.Length);
        return Convert.ToBase64String(output);
    }

    public static string Decrypt(byte[] key, string info)
    {
        var des = CreateTripleDES(key);
        var ct = des.CreateDecryptor();
        var input = Convert.FromBase64String(info);
        var output = ct.TransformFinalBlock(input, 0, input.Length);
        return Encoding.UTF8.GetString(output);
    }

    private static TripleDES CreateTripleDES(byte[] key)
    {
        TripleDES des = TripleDES.Create();
        var desKey = MD5.HashData(key);
        des.Key = desKey;
        des.IV = new byte[des.BlockSize / 8];
        des.Padding = PaddingMode.PKCS7;
        des.Mode = CipherMode.ECB;
        return des;
    }
}