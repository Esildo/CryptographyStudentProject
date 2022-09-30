using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Security.Cryptography;
namespace CryptoSymm.AsynEnc
{


    public static class KeyGeneration
    {
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


        public static BigInteger GenerationRandInt()
        {
            var rand = new Random();
            byte[] bytes = new byte[2];
            rand.NextBytes(bytes);
            bytes[^1] &= 0b01111111;
            bytes[0] |= 0b00000001;
            BigInteger q = new BigInteger(bytes);


            return q;
        }

        private static void GenerationSimpleInteger(ref BigInteger q, ref BigInteger p, BigInteger r)
        {
            p = GenerationRandInt();
            q = GenerationRandInt();
            while ((FermTest.GetTest(q, 0.9) != true) || (BigInteger.GreatestCommonDivisor(r, q - 1) != 1) ||  (q < 1))
            {
                q = GenerationRandInt();
            }
            while (true)
            {
                if((FermTest.GetTest(p, 0.9) == true) && (BigInteger.GreatestCommonDivisor(r, ((p - 1) / r)) == 1) && (((p - 1) % r) == 0) && (p > 1))
                {
                    break;
                }
                p = GenerationRandInt();
            }
            Console.WriteLine($"P-1 % R!!!{(p-1) % r}");
            Console.WriteLine($"GREAT DIVISOR {BigInteger.GreatestCommonDivisor(r,q-1)} This is p {p} this is {q}");
        }

        public static Key Create_key( BigInteger r)
        {
            BigInteger p = new BigInteger();
            BigInteger q = new BigInteger();
            BigInteger n = new BigInteger();
            BigInteger f = new BigInteger();
            BigInteger y = new BigInteger();
            BigInteger x = new BigInteger(); 


            GenerationSimpleInteger(ref q, ref p, r);
 
            n = p * q;
            f = (p - 1)*(q - 1);
            
            do
            {
                y = GetRandomInteger(2, n - 1);
            } while (BigInteger.ModPow(y, f / r, n) == 1 || (BigInteger.GreatestCommonDivisor(y, n) != 1));

            x = BigInteger.ModPow(y, f / r, n);

            //while ( (( BigInteger.ModPow(y, f / r, n)) == 1) || (BigInteger.GreatestCommonDivisor(y, n) != 1)|| (y == 0) ||(y == 1) )
            //{
            //    y = GenerationRandInt() % n;
            //    Console.WriteLine($"X is {x}");

            //}
          
            Console.WriteLine($"BigInteger.ModPow(y, f / r, n)){BigInteger.ModPow(y, f / r, n) == 1} {BigInteger.GreatestCommonDivisor(r, ((p - 1) / r)) == 1}");
            Console.WriteLine($"X is {x}");
            return new Key(y, n, f, x);
            
        }

        
        
    }
}
