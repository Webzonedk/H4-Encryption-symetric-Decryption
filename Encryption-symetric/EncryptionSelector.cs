using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Encryption_symetric
{
    internal class EncryptionSelector
    {
        public static SymmetricAlgorithm algo;
        public static void Selector(int select)
        {

            switch (select)
            {
                case 0:
                    algo = Aes.Create();
                    algo.KeySize= 128;
                    algo.Mode = CipherMode.CBC;
                    break;

                case 1:
                    algo = Aes.Create();
                    algo.KeySize = 128;
                    algo.Mode = CipherMode.ECB;
                    break;

                case 2:
                    algo = Aes.Create();
                    algo.KeySize = 192;
                    algo.Mode = CipherMode.CBC;
                    break;

                case 3:
                    algo = Aes.Create();
                    algo.KeySize = 192;
                    algo.Mode = CipherMode.ECB;
                    break;

                case 4:
                    algo = Aes.Create();
                    algo.KeySize = 256;
                    algo.Mode = CipherMode.CBC;
                    break;
                    
                case 5:
                    algo = Aes.Create();
                    algo.KeySize = 256;
                    algo.Mode = CipherMode.ECB;
                    break;
                       
                case 6:
                    algo = TripleDES.Create();
                    algo.KeySize = 128;
                    algo.Mode = CipherMode.CBC;
                    break;
                                           
                case 7:
                    algo = TripleDES.Create();
                    algo.KeySize = 128;
                    algo.Mode = CipherMode.ECB;
                    break;
                                           
                case 8:
                    algo = TripleDES.Create();
                    algo.KeySize = 192;
                    algo.Mode = CipherMode.CBC;
                    break;
                                           
                case 9:
                    algo = TripleDES.Create();
                    algo.KeySize = 192;
                    algo.Mode = CipherMode.ECB;
                    break;
                                           
                case 10:
                    algo = TripleDES.Create();
                    algo.KeySize = 256;
                    algo.Mode = CipherMode.CBC;
                    break;
                                           
                case 11:
                    algo = TripleDES.Create();
                    algo.KeySize = 256;
                    algo.Mode = CipherMode.ECB;
                    break;
            }
        }
    }
}
