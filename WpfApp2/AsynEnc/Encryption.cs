using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Security.Cryptography;

namespace CryptoSymm.AsynEnc
{
    public class Encryption
    {

        public static BigInteger[] Enc(byte[] M, BigInteger y, BigInteger r, BigInteger n)
        {

            BigInteger[] C = new BigInteger[M.Length];



            //BigInteger u = BigInteger.ModPow(KeyGeneration.GenerationRandInt(), 1, n);
            BigInteger u = GetRandomInteger(2,n-1) ;
         
            BigInteger E = new BigInteger();
            for(var i = 0; i < M.Length; i++)
            {
                E = (BigInteger.ModPow(y, (int)M[i], n) * BigInteger.ModPow(u, (int)r, n)) % n;
                C[i] = E;
            }



            //E = (BigInteger.ModPow(y, (int)M[i],n) * BigInteger.ModPow(u, (int)r,n)) % n ; /*(BigInteger.ModPow(BigInteger.Pow(y, (int)M) * BigInteger.Pow(u, (int)r), 1, n))*/;
            //Console.WriteLine(E);

            return C;

        }
        public static BigInteger GetRandomInteger(BigInteger a, BigInteger b)
        {
            BigInteger number = new BigInteger();
            var generated = RandomNumberGenerator.Create();
            byte[] bytesArray = b.ToByteArray();

            do
            {
                generated.GetBytes(bytesArray);
                number = new BigInteger(bytesArray);
            } while (!(number >= a && number < b));

            return (number);
        }




    }
}
