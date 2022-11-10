using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Encryption_symetric
{
    internal class Decrypt
    {
        internal (string, string) LetsDecryptSomething(int selected, string input, byte[] key, byte[] IV)
        {
            string decryptedAscii="Hov Du glemte vist at indsætte en krypteret besked";
            string decryptedHex = "Hov Du glemte vist at indsætte en krypteret besked";
            try
            {
                if (selected < 6)
                {
                    byte[] inputBytes = Convert.FromBase64String(input);
                    string decryptedString = DecryptBytesToString_Aes(inputBytes, key, IV);
                    //Converting to hex and removing the "-"
                    byte[] decryptedBytes = Convert.FromBase64String(decryptedString);
                    decryptedHex = BitConverter.ToString(decryptedBytes).Replace("-", String.Empty);

                    decryptedAscii = decryptedString;
                }
                else
                {
                    string encryptedFile = "../../EncryptionFile.enc";

                    string decryptedString = DecryptDataToFile_3DES(encryptedFile, key, IV);

                    decryptedAscii = decryptedString;
                    //Converting to hex and removing the "-"
                    byte[] encryptedBytes = Encoding.Default.GetBytes(decryptedString);
                    decryptedHex = BitConverter.ToString(encryptedBytes).Replace("-", String.Empty);

                }
                return (decryptedAscii, decryptedHex);

            }
            catch (Exception)
            {

                return (decryptedAscii, decryptedHex);
            }

        }

        static string DecryptBytesToString_Aes(byte[] input, byte[] key, byte[] iv)
        {
                string decryptedText = "";
            try
            {
                // Checking arguments. (friendly lend to my by Microsoft, such a friendly company.)
                if (input == null || input.Length <= 0)
                    throw new ArgumentNullException("cipher text is not valid");
                if (key == null || key.Length <= 0)
                    throw new ArgumentNullException("Key is not valid");
                if (iv == null || iv.Length <= 0)
                    throw new ArgumentNullException("IV is not valid");


                using (EncryptionSelector.algo)
                {

                    //Creating the incryptor
                    ICryptoTransform decryptor = EncryptionSelector.algo.CreateDecryptor(key, iv);

                    //Create streams
                    using (MemoryStream memoryStreamDecryptor = new(input))
                    {
                        using (CryptoStream cryptoStream = new(memoryStreamDecryptor, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader streamReaderDecrypt = new(cryptoStream))
                            {
                                //Read the decrypted bytes, and adding them to a string
                                decryptedText = streamReaderDecrypt.ReadToEnd();
                            }
                        }
                    }
                }

                // Return the decrypted text from the memory stream.
                return decryptedText;

            }
            catch (Exception)
            {

                return decryptedText;

            }
        }




        //Obsolete, and should not be used, but is included for educational purposes
        static string DecryptDataToFile_3DES(string input, byte[] key, byte[] iv)
        {

            try
            {
                //Opening the file
                using (FileStream fileStream = File.OpenRead(input))

                //using the 3DES_algo, created in the selector
                using (EncryptionSelector.algo)

                //create an decrypter, using the key and iv
                using (ICryptoTransform decryptor = EncryptionSelector.algo.CreateDecryptor(key, iv))

                //Creating the cryptostream using filestream and encryptor
                using (CryptoStream cryptoStream = new(fileStream, decryptor, CryptoStreamMode.Read))

                using (StreamReader streamReaderDecrypt = new(cryptoStream, Encoding.UTF8))
                {

                    return streamReaderDecrypt.ReadToEnd();

                }
            }
            catch (CryptographicException e)
            {
                return "";
            }
        }
    }
}
