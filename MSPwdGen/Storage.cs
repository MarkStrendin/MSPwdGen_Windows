using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.IsolatedStorage;
using System.IO;

namespace MSPwdGen
{
    class Storage
    {
        const string ConfigFileName = "MarkPasswordGen.blob";

        public static void setSalt(string input)
        {
            using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null))
            {
                using (IsolatedStorageFileStream oStream = new IsolatedStorageFileStream(ConfigFileName, FileMode.Create, isoStore))
                {
                    using (StreamWriter writer = new StreamWriter(oStream))
                    {                       
                        if ((input.Length > 0))
                        {
                            writer.Write(input);
                        }
                        else
                        {
                            writer.Write(Crypto.convertByteArrayToString(Crypto.hashThis_SHA512(DateTime.Now.ToString())));
                        }
                        writer.Close();
                    }
                }
            }
        }

        public static string getSalt()
        {
            string returnMe = "";
            using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null))
            {
                string[] fileNames = isoStore.GetFileNames(ConfigFileName);

                Boolean foundFile = false;

                foreach (string file in fileNames)
                {
                    if (file == ConfigFileName)
                    {
                        foundFile = true;
                        //The file exists
                    }
                }

                if (foundFile == false)
                {
                    generateNewSaltFile();
                    return getSalt();
                }
                else
                {
                    using (IsolatedStorageFileStream iStream = new IsolatedStorageFileStream(ConfigFileName, System.IO.FileMode.Open, isoStore))
                    {
                        StreamReader reader = new StreamReader(iStream);
                        String line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            returnMe = line;
                        }                        
                    }
                }
            }
            return Crypto.decryptThis(returnMe);
        }

        /// <summary>
        /// Generates a new salt file, if one doesn't already exist
        /// </summary>
        private static void generateNewSaltFile()
        {
            using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null))
            {
                using (IsolatedStorageFileStream oStream = new IsolatedStorageFileStream(ConfigFileName, FileMode.Create, isoStore))
                {
                    using (StreamWriter writer = new StreamWriter(oStream))
                    {
                        writer.Write(Crypto.convertByteArrayToString(Crypto.hashThis_SHA512(DateTime.Now.ToString())));
                        writer.Close();
                    }                    
                }
            }


        }
    }
}
