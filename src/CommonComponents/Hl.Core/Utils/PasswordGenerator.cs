using System;

namespace Hl.Core.Utils
{
    public static class PasswordGenerator
    {
        private const string pwdSeedLine = "123456789abcdefghijklmnpqrstuvwxyzABCDEFGHIJKLMNPQRSTUVWXYZ!@#$%^&*";
        public static string GetRandomPwd(int len)
        {
            string reValue = string.Empty;
            Random rnd = new Random(GetNewSeed());
            while (reValue.Length < len)
            {
                string s1 = pwdSeedLine[rnd.Next(0, pwdSeedLine.Length)].ToString();
                if (reValue.IndexOf(s1) == -1) reValue += s1;
            }
            return reValue;
        }

        private static int GetNewSeed()
        {
            byte[] rndBytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(rndBytes);
            return BitConverter.ToInt32(rndBytes, 0);
        }
    }
}
