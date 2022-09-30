using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoSymm.AsynEnc;
using  CryptoSymm.Magenta;
using CryptoSymm.Magenta.Magenta;
using System.IO;

namespace CryptoSymm
{
    internal class Encrypt_File
    {

        public byte[] Encrypt(MagentKeyGeneration SymKey,string path,string pathTo,int countByte)
        {
            //Benalo Asyn = new Benalo();
            Magentas Magent = new Magentas();
            //MagentKeyGeneration SymKey = new MagentKeyGeneration();

            FileStream stream = new FileStream(path, FileMode.Open);
            FileStream fs = new FileStream(pathTo, FileMode.Create);

            int x;
            int bytes = 0;

            byte[] buffer = new byte[countByte];
            byte[] z = new byte[countByte];
            try
            {
                while (bytes < stream.Length)
                {

                    x = stream.Read(buffer, 0, countByte);

                    //fs.Write(buffer, 0, 12);

                    

                    if (x < countByte)
                    {

                        for (var i = 0; i < countByte - x; i++)
                        {
                            buffer[i + x] = 0;
                        }
                        z = Magent.FinalFunc(buffer, SymKey.AKey, 2);
                        fs.Write(z, 0, countByte);
                    }
                    else
                    {
                        z = Magent.FinalFunc(buffer, SymKey.AKey, 2);
                        fs.Write(z, 0, countByte);
                    }

                    bytes += countByte;

                }
            
             }
            finally
            {
                ;
            }
            fs.Close();
            stream.Close();

            return SymKey.AKey;
        }
        
    }
}
