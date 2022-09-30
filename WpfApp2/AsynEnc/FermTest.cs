using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace CryptoSymm.AsynEnc
{
    internal static class FermTest
    {
        public static bool GetTest(BigInteger N,double probabylity)
        {
            
            int k;
            BigInteger b = new BigInteger();
            if(probabylity <= 0.8 || probabylity > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(N), "probability not good");
            }
            if (N == 1)
            {
                return false;
            }
           
            else
            {
                for (int i = 2; 1 - Math.Pow(2, -i) <= probabylity; i++)
                {

                    BigInteger a = GetA(N);
                    b = BigInteger.ModPow(a , N - 1, N);
                    if ((b != 1) || (b == 561) || (b == 41041) || (b ==825265) || (b == 321197185) || (b ==5394826801) || (b == 232250619601) || (b == 9746347772161) )
                    {
                        return false;
                    }

                }
                return true;
            }
        }

        public static BigInteger GetA (BigInteger N)
        {
            BigInteger a = new BigInteger();
            a = KeyGeneration.GenerationRandInt();
            while( a > N - 1 || a < 2)
            {
                a = KeyGeneration.GenerationRandInt();
            }
            return a;
           

        }

    }
}
