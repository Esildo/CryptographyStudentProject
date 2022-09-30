using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using CryptoSymm.AsynEnc;

namespace CryptoSymm.AsynEnc
{
    public class PublicKey
    {
        public BigInteger y { set; get; }
        public BigInteger r { set; get; }
        public BigInteger n { set; get; }
        public PublicKey(BigInteger y, BigInteger r, BigInteger n)
        {
            this.y = y;
            this.r = r;
            this.n = n;
        }


    }

    public class Key
    {
        public BigInteger y { private set; get; }
        public BigInteger r { private set; get; }
        public BigInteger n { private set; get; }
        public BigInteger f { private set; get; }
        public BigInteger x { private set; get; }
        public Key(BigInteger y,  BigInteger n,BigInteger f ,BigInteger x)
        {
            this.y = y;
            this.n = n;
            this.f = f;
            this.x = x;
            this.r = 277;
        }

        public PublicKey GetPublic()
        {
            return new PublicKey(this.y,this.r,this.n);
        }
      
    }

   
  

    public class Benalo
    {
        //public Key K = KeyGeneration.Create_key(277);
        
      

        public BigInteger[] EncryprionFunc(byte[] M, BigInteger y , BigInteger r, BigInteger n)
        {
            

            return Encryption.Enc(M, y, r, n);
        }

        public byte[] Deryption(BigInteger[] C, BigInteger f , BigInteger r, BigInteger x, BigInteger n )
        {
            byte E = new byte();
            byte[] M = new byte[C.Length];
            for (var i = 0; i < C.Length; i++)
            {
                E = Decrypt.Decryption(C[i], f, r, x, n);
                M[i] = E;
            }
            
            return M;

        }
        
    }
}
