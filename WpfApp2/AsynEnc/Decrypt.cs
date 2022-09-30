using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace CryptoSymm.AsynEnc
{
    class Decrypt
    {
        public static byte Decryption(BigInteger c, BigInteger f, BigInteger r, BigInteger x, BigInteger n)
        {
            var a = BigInteger.ModPow(c, f / r, n);
            
            for (byte i = 0; i < r; i++)
            {
                if (BigInteger.ModPow(x, i, n) == a)
                {
                 
                    return i;
                }
            }

            return 1;
        }
    }
}
