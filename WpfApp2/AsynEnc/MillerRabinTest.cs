using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace CryptoSymm.AsynEnc
{
    internal static class MillerRabinTest
    {
        public static bool GetTest(BigInteger N)
        {
            N.Get_d_s(out var d, out var s);
            var k = (int)BigInteger.Log(N, 2.0);
            var isPrime = true;
            Parallel.For(0, k, (i, pls) =>
            {
                var a = BigInteger.ModPow(BigRandInt(), d, N);
                if (a == 1 || a == N - 1) return;
                for (var r = 1; r < s; r++)
                    if ((a = BigInteger.ModPow(a, 2, N)) == N - 1) return;
                isPrime = false;
                pls.Break();
            });
            return isPrime;

        }

        private static void Get_d_s(this BigInteger n,out BigInteger d,out int s)
        {
            s = 0;
            d = n - 1;
            while(d%2 != 1)
            {
                d /= 2;
                s++;
            }
        }
        private static BigInteger BigRandInt()
        {
            var rand = new Random();
            byte[] bytes = new byte[8];
            rand.NextBytes(bytes);
            BigInteger q = new BigInteger(bytes);
            return q;
        }
    }
}
