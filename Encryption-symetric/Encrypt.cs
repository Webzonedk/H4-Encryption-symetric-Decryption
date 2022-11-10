using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace Encryption_symetric
{
    internal class Encrypt
    {
        internal (string, string) LetsEncryptSomething(int selected, string input, byte[] key, byte[] IV)
        {
            string encryptedAscii="Hov Du glemte vist at indsætte en krypteret besked"; ;
            string encryptedHex= "Hov Du glemte vist at indsætte en krypteret besked"; ;
            try
            {
                if (selected < 6)
                {
                    byte[] encrypted = EncryptStringToBytes_Aes(input, key, IV);
                    //Converting to hex and removing the "-"
                    encryptedHex = BitConverter.ToString(encrypted).Replace("-", String.Empty);
                    encryptedAscii = Convert.ToBase64String(encrypted);
                }
                else
                {
                    string encryptedFile = "../../EncryptionFile.enc";

                    EncryptDataToFile_3DES(input, encryptedFile, key, IV);

                    string encryptedString = File.ReadAllText(encryptedFile);

                    encryptedAscii = encryptedString;
                    //Converting to hes and removing the "-"
                    byte[] encryptedBytes = Encoding.Default.GetBytes(encryptedString);
                    encryptedHex = BitConverter.ToString(encryptedBytes).Replace("-", String.Empty);

                }
                return (encryptedAscii, encryptedHex);
            }
            catch (Exception)
            {

                return (encryptedAscii, encryptedHex);
            }
        }

        static byte[] EncryptStringToBytes_Aes(string input, byte[] key, byte[] iv)
        {
            try
            {
                // Checking arguments. (friendly lend to my by Microsoft, such a friendly company.)
                if (input == null || input.Length <= 0)
                    throw new ArgumentNullException("plainText is not valid");
                if (key == null || key.Length <= 0)
                    throw new ArgumentNullException("Key is not valid");
                if (iv == null || iv.Length <= 0)
                    throw new ArgumentNullException("IV is not valid");


                using (EncryptionSelector.algo)
                {
                    //Creating the incryptor
                    ICryptoTransform encryptor = EncryptionSelector.algo.CreateEncryptor(key, iv);

                    //Create streams
                    using (MemoryStream memoryStreamEncryptor = new())
                    {
                        using (CryptoStream cryptoStream = new(memoryStreamEncryptor, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter writer = new(cryptoStream))
                            {
                                //Writing the data to the stream
                                writer.Write(input);
                            }
                            return memoryStreamEncryptor.ToArray();
                        }
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }




        //Obsolete, and should not be used, but is included for educational purposes
        static void EncryptDataToFile_3DES(string input, string outputFile, byte[] key, byte[] iv)
        {

            try
            {
                //Create or open the encrypton file
                using (FileStream fileStream = File.Open(outputFile, FileMode.Create))
                //using the algo, created in the selector
                using (EncryptionSelector.algo)
                //create an encrypter, using the key and iv
                using (ICryptoTransform encryptor = EncryptionSelector.algo.CreateEncryptor(key, iv))
                //Creating the filestream using filestream and encryptor
                using (CryptoStream cryptoStream = new(fileStream, encryptor, CryptoStreamMode.Write))
                {
                    //Convert string to bytes
                    byte[] stringToEncryptInBytes = Encoding.UTF8.GetBytes(input);

                    //Write the byte array to crypto stream
                    cryptoStream.Write(stringToEncryptInBytes, 0, stringToEncryptInBytes.Length);
                }
            }
            catch (CryptographicException e)
            {
                throw;
            }
        }
    }
}
