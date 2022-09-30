using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoSymm.AsynEnc;
using CryptoSymm.Magenta;
using CryptoSymm.Magenta.Magenta;
using System.IO;

namespace CryptoSymm
{
    internal class Decrypt_file
    {
        public void Decrypt(string path, string pathTo, byte[] Akey ,int countByte)
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            FileStream fs = new FileStream(pathTo, FileMode.Create);
            //Benalo Asyn = new Benalo();
            Magentas Magent = new Magentas();

            

            int x;
            int bytes = 0;

            byte[] buffer = new byte[countByte] ;
            
            byte[] z = new byte[countByte];
            try
            {
                while (bytes < stream.Length)
                {

                    x = stream.Read(buffer, 0, countByte);

                    //fs.Write(buffer, 0, 12);

                    

                    //if (x < countByte)
                    //{
                    //    for (var i = 0; i < countByte; i++)
                    //    {
                    //        buffer[i] = 254;
                    //    }

                    //    z = Magent.Decrypt(Magent, buffer, Akey, 2);
                    //    fs.Write(z, 0, countByte);
                    //    Console.WriteLine("HEEEEEEEEERE");
                    //}
                    //else
                    //{
                        z = Magent.Decrypt(Magent, buffer, Akey, 2);

                        fs.Write(z, 0, countByte);
                    //}

                    bytes += countByte;

                }

            }
            finally
            {
                ;
            }
            Console.WriteLine($"BYTES {bytes},LENGTH ! {stream.Length}");
            fs.Close();
            stream.Close();

        }
    }
}
