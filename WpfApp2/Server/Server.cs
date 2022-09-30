using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoSymm.AsynEnc;
using CryptoSymm.Magenta;
using CryptoSymm.Magenta.Magenta;
using System.IO;
using System.Numerics;
using CryptoSymm; 

namespace WpfApp2.Servers
{
    internal class Server
    {

        private Benalo Ben;
        Magentas Magent;
        private Key Ka;
        Decrypt_file File_decrypt;
        
        public Server()
        {
            Ben = new Benalo();
            Magent = new Magentas();
            File_decrypt = new Decrypt_file();
            Ka = KeyGeneration.Create_key(277);
        }



        //private Benalo Ben = new Benalo();
        //Magentas Magentas = new Magentas();
        //private Key Ka = KeyGeneration.Create_key(277);
        public PublicKey SendPublicKey()
        {
            return Ka.GetPublic();
        }
        
        public void File_Decrypt(string path, string pathTo, BigInteger[] SymEncKey, int countByte)
        {
            byte[] DecKey = Ben.Deryption(SymEncKey, Ka.f, Ka.r, Ka.x, Ka.n);
            File_decrypt.Decrypt(path,pathTo, DecKey, countByte);        
        }
       



    }
}
