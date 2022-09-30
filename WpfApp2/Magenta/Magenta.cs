using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;


namespace CryptoSymm.Magenta
{
    public class Magentas
    {
        private static T[] Concat<T>(params T[][] arrays)
        {
            var result = new T[arrays.Sum(a => a.Length)];
            int offset = 0;
            foreach (T[] array in arrays)
            {
                array.CopyTo(result, offset);
                offset += array.Length;
            }
            return result;
        }


        static byte[] s_block = new byte[256]
                    { 1,2,4,8,16,32,64,128,
                     101,202,241,135,107,214,201,247,
                     139,  115,  230,  169,   55,  110,  220,  221,
                     223,  219,  211,  195,  227,  163,   35,   70,
                     140,  125,  250,  145,   71,  142,  121,  242,
                     129,  103,  206,  249,  151,   75,  150,   73,
                     146,   65,  130,   97,  194,  225,  167,   43,
                      86,  172,   61,  122,  244,  141,  127,  254,
                     153,   87,  174,   57,  114,  228,  173,   63,
                     126,  252,  157,   95,  190,   25,   50,  100,
                     200,  245,  143,  123,  246,  137,  119,  238,
                     185,   23,   46,   92,  184,   21,   42,   84,
                     168,   53,  106,  212,  205,  255,  155,   83,
                     166,   41,   82,  164,   45,   90,  180,   13,
                      26,   52,  104,  208,  197,  239,  187,   19,
                      38,   76,  152,   85,  170,   49,   98,  196,
                     237,  191,   27,   54,  108,  216,  213,  207,
                     251,  147,   67,  134,  105,  210,  193,  231,
                     171,   51,  102,  204,  253,  159,   91,  182,
                       9,   18,   36,   72,  144 ,  69,  138,  113,
                     226,  161,   39,   78,  156,   93 , 186 ,  17,
                      34,   68,  136,  117,  234,  177 ,   7 ,  14,
                      28,   56,  112,  224,  165,   47 ,  94 , 188,
                      29,   58,  116,  232,  181,   15 ,  30 ,  60,
                     120,  240,  133,  111,  222,  217 , 215 , 203,
                     243,  131,   99,  198,  233,  183 ,  11 ,  22,
                      44,   88,  176,    5,   10,   20 ,  40 ,  80,
                     160,   37,   74,  148,   77,  154 ,  81 , 162,
                      33,   66,  132,  109,  218,  209 , 199 , 235,
                     179,    3,    6,   12,   24,   48 ,  96 , 192,
                     229,  175,   59,  118,  236,  189 ,  31 ,  62,
                     124,  248,  149,   79,  158,   89 , 178 ,   0 
                    };
            

                
    private static byte F(byte x)
        {

            return s_block[x];
        }

        private static byte A(byte x, byte y)
        {


            byte b = F((byte)(x ^ F(y)));

            return b;
        }

        private static byte[] PE(byte x, byte y)
        {
            byte[] c = new byte[2] { A(x, y), A(y, x) };
            return c;
        }

        private static byte[] TT(byte[] X)
        {
            byte[] N = new byte[X.Length];
            int a = 2;

            for (int i = 0; i < X.Length / 2; i++)
            {
                if (i == 0)
                {
                    PE(X[i], X[i + X.Length / 2]).CopyTo(N, 0);
                }
                else
                {
                    PE(X[i], X[i + X.Length / 2]).CopyTo(N, a);
                    a += 2;
                }
               
            }
            return N;
        }
        private static byte[] T(byte[] X)
        {

            for (int i = 0; i < 4; i++)
            {
                X = TT(X);
            }

            return X;
        }
        private static byte[] S(byte[] X)
        {

            byte[] buf = new byte[X.Length];
            int a = 0;
            int b = X.Length / 2;
            for (int i = 0; i < X.Length; i++)
            {

                if (i % 2 == 0)
                {
                    buf[a] = X[i];
                    a++;
                }
                else
                {
                    buf[b] = X[i];
                    b++;
                }

            }

            return buf;
        }
        private static byte[] C(int k, byte[] X)
        {
            byte[] funcbuf;
            byte[] buf = new byte[X.Length];
            if (k == 1)
            {
                buf = T(X);
                return buf;
            }
            else
            {
                funcbuf = S(C(k - 1, X));
                //BigInteger x = new BigInteger(X);
                //BigInteger s = new BigInteger(S(C(k - 1, X)));
                //BigInteger z = x ^ s;
                //buf = z.ToByteArray();
                for (var i = 0; i < X.Length; i++)
                {
                    buf[i] = (byte)(funcbuf[i] ^ X[i]);
                }
                return T(buf);

            }
        }



        private static byte[] XOR(byte[] A, byte[] B)
        {
            byte[] F = new byte[A.Length ];

            for(var i = 0; i < A.Length;i++)
            {
              F[i] = (byte)(A[i] ^ B[i]);
            }
            
            //BigInteger a = new BigInteger(A);
            //BigInteger b = new BigInteger(B);
            //BigInteger z = a ^ b;
            //F = z.ToByteArray();
            //Console.WriteLine($" F length {A.Length}");
            //Console.WriteLine($" B length {B.Length}");
            //Console.WriteLine($" F length {F.Length}");
            return F;
        }
        private static byte[] Feistel_round(byte[] X, byte[] Y)
        {
            byte[] X1 = new byte[X.Length / 2];
            byte[] X2 = new byte[X.Length / 2];
            byte[] N = new byte[X.Length];


            Devide(ref X, ref X1, ref X2, 2);
            N = Concat(X2, Y);
            //Console.WriteLine(String.Join(",", N));
            byte[] F = new byte[X.Length];
            F = Concat(X2, XOR(X1, C_e(S(C(3, N)))));

            return F;
        }
        public static void Devide(ref byte[] X, ref byte[] X1, ref byte[] X2, int part)
        {
            if (part == 2)
            {
                int mid = (X.Length + 1) / 2;
                X1 = X[..mid];
                X2 = X[mid..];
            }

        }
        private static byte[] C_e(byte[] X)
        {
            byte[] Z = new byte[X.Length / 2];
            for (int i = 0; i < X.Length / 2; i++)
            {
                Z[i] = X[i];
            }
            return Z;
        }
        public byte[] FinalFunc(byte[] X, byte[] K, int count_key)
        {
            byte[] K1 = new byte[K.Length / 2];
            byte[] K2 = new byte[K.Length / 2];
            Devide(ref K, ref K1, ref K2, 2);
            byte[] X1 = new byte[X.Length / 2];
            byte[] X2 = new byte[X.Length / 2];
            Devide(ref X, ref X1, ref X2, 2);


            for (int i = 0; i < 6; i++)
            {
                if (i < 2 || i >= 4)
                {
                    X = Feistel_round(X, K1);
                }
                else if (i >= 2 && i < 4)
                {
                    X = Feistel_round(X, K2);
                }

            }

            return X;

        }

        public  byte[] Decrypt(Magentas Mag,byte[] M, byte[] K, int count_key)
        {
            return V(Mag.FinalFunc(V(M), K, count_key));
        }

        private static byte[] V(byte[] X)
        {
            byte[] buf = new byte[X.Length];

            for (int i = 0; i < X.Length; i++)
            {
                buf[i] = X[(i + X.Length / 2) % 16];
            }
            return buf;
        }
    }
}
