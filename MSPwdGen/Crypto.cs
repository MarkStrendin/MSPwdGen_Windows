using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace MSPwdGen
{
    static class Crypto
    {
        public static string createPassword_Alpha(string input, string salt)
        {
            char[] characterArray_Alpha = {'1','2','3','4','5','6','7','8','9','0','a','b','c','d','e','f',
                                            'g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v',
                                            'w','x','y','z','A','B','C','D','E','F','G','H','I','J','K','L',
                                            'M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};

            return genPasswordWithThisHash(characterArray_Alpha, hashThis_SHA512(input + salt));
        }

        public static string createPassword_Special(string input, string salt)
        {
            char[] characterArray_Special = {'1','2','3','4','5','6','7','8','9','0','a','b','c','d','e','f',
                                            'g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v',
                                            'w','x','y','z','A','B','C','D','E','F','G','H','I','J','K','L',
                                            'M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z','!','@','#',
                                            '$','%','^','*','(',')','_','+','?'};

            return genPasswordWithThisHash(characterArray_Special, hashThis_SHA512(input + salt));
        }

        private static string genPasswordWithThisHash(char[] characterSet, byte[] input)
        {           
            string returnMe = String.Empty;
            foreach (byte thisByte in input)
            {
                int thisInt = (int)thisByte;
                while (thisInt > characterSet.Length - 1) 
                {
                    thisInt -= characterSet.Length;
                }
                returnMe += characterSet[thisInt];
            }             
            return returnMe;
        }

        public static byte[] hash(string hashThis)
        {
            return hashThis_SHA512(hashThis);
        }

        private static byte[] hashThis_SHA512(string hashThis)
        {
            /* Should be using ASCII encoding instad of Unicode */
            //UnicodeEncoding UE = new UnicodeEncoding();

            byte[] hashValue;
            //byte[] message = UE.GetBytes(hashThis);
            byte[] message = Encoding.ASCII.GetBytes(hashThis);

            SHA512Managed hashString = new SHA512Managed();
            hashValue = hashString.ComputeHash(message);

            return hashValue;
        }

        private static byte[] hashThis_SHA256(string hashThis)
        {
            /* Should be using ASCII encoding instad of Unicode */
            UnicodeEncoding UE = new UnicodeEncoding();

            byte[] hashValue;
            //byte[] message = UE.GetBytes(hashThis);
            byte[] message = Encoding.ASCII.GetBytes(hashThis);

            SHA256Managed hashString = new SHA256Managed();
            
            hashValue = hashString.ComputeHash(message);            
            return hashValue;
        }

        public static string convertByteArrayToString(byte[] convertThis)
        {
            string returnMe = String.Empty;

            foreach (byte x in convertThis)
            {
                returnMe += String.Format("{0:x2}",x);
            }
            return returnMe;
        }

        public static byte[] encrypt(byte[] clearData, byte[] Key, byte[] IV)
        {
            MemoryStream ms = new MemoryStream();
            Rijndael alg = Rijndael.Create();
            alg.Key = Key;
            alg.IV = IV;

            CryptoStream cs = new CryptoStream(ms,alg.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(clearData, 0, clearData.Length);
            cs.Close();

            byte[] encryptedData = ms.ToArray();
            return encryptedData;
        }

        public static byte[] decrypt(byte[] cipherData, byte[] Key, byte[] IV)
        {
            MemoryStream ms = new MemoryStream();

            Rijndael alg = Rijndael.Create();

            alg.Key = Key;
            alg.IV = IV;

            CryptoStream cs = new CryptoStream(ms,
                alg.CreateDecryptor(), CryptoStreamMode.Write);

            cs.Write(cipherData, 0, cipherData.Length);
            cs.Close();
            byte[] decryptedData = ms.ToArray();
            return decryptedData;
        }

        public static string encrypt(string plainText, string sharedSecret)
        {
            byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(plainText);           
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(sharedSecret,
                new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 
            0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});

            byte[] encryptedData = encrypt(clearBytes,
                     pdb.GetBytes(32), pdb.GetBytes(16));

            return Convert.ToBase64String(encryptedData); 
        }

        public static string decrypt(string cipherText, string Password)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65,0x64, 0x76, 0x65, 0x64, 0x65, 0x76});

            byte[] decryptedData = decrypt(cipherBytes, pdb.GetBytes(32), pdb.GetBytes(16));

            return System.Text.Encoding.Unicode.GetString(decryptedData);
        }


    }    
}
