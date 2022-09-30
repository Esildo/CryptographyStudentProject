using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Security.Cryptography;


namespace CryptoSymm.Magenta.Magenta
{
    internal class MagentKeyGeneration
    {
        public MagentKeyGeneration()
        {
            AKey = GenKey() ;
        }
        private byte[] _key;
        private static byte[] GenKey()
        {

            var rand = new Random();
            byte[] bytes = new byte[16];
            rand.NextBytes(bytes);
            return bytes;

        }

        public byte[] AKey
        {
            get 
            {
                return _key; 
            }

             set
            {
                _key = value;
            }
        
        }


    }
}
