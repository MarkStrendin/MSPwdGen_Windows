using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

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

            return genPasswordWithThisHash(characterArray_Alpha, hashThis_SHA256(input + salt));
        }

        public static string createPassword_Special(string input, string salt)
        {
            char[] characterArray_Special = {'1','2','3','4','5','6','7','8','9','0','a','b','c','d','e','f',
                                            'g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v',
                                            'w','x','y','z','A','B','C','D','E','F','G','H','I','J','K','L',
                                            'M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z','!','@','#',
                                            '$','%','^','*','(',')','_','+','?'};

            return genPasswordWithThisHash(characterArray_Special, hashThis_SHA256(input + salt));
        }

        private static string genPasswordWithThisHash(char[] characterSet, byte[] input)
        {           
            string returnMe = "";
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
        
        private static byte[] hashThis_SHA512(string hashThis)
        {
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            byte[] message = UE.GetBytes(hashThis);

            SHA512Managed hashString = new SHA512Managed();
            hashValue = hashString.ComputeHash(message);

            return hashValue;
        }

        private static byte[] hashThis_SHA256(string hashThis)
        {
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            byte[] message = UE.GetBytes(hashThis);

            SHA256Managed hashString = new SHA256Managed();
            
            hashValue = hashString.ComputeHash(message);            
            return hashValue;
        }

        public static string encryptThis(string input) 
        {
            return "TODO";
        }

        public static string decryptThis(string input) 
        {
            return "TODO";
        }

    }

    
}
