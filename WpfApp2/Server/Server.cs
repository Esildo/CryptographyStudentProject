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
namespace WpfApp2.Servers
{
    internal class Server
    {
        private Benalo Ben = new Benalo();
        Magentas Magentas = new Magentas();
        private Key Ka = KeyGeneration.Create_key(277);
        public PublicKey SendPublicKey()
        {
            return Ka.GetPublic();
        }
        
       



    }
}
