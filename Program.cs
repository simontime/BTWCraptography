using System;
using System.IO;
using System.Text;

namespace BTWCraptography
{
    class Program
    {
        static void CraptographicFunction(FileStream Input, FileStream Output, byte[] Key)
        {
            using (var Reader = new BinaryReader(Input))
            {
                for (int i = 0, Ctr = 0; i < Input.Length; i++)
                {
                    var Transformed = (byte)((Key[Ctr] ^ Reader.ReadByte()) ^ (byte)(Ctr - 0x21));

                    if (Ctr == Key.Length - 1) Ctr = 0;
                    else Key[Ctr] += Key[Ctr++ + 1];

                    Output.WriteByte(Transformed);
                }

                Output.Close();
            }
        }

        static void Main(string[] args)
        {
            if (args.Length != 2)
                Console.WriteLine("Usage: BTWCraptography.exe <Input file> <ASCII \"Key\">");

            CraptographicFunction
            (
                File.OpenRead(args[0]),
                File.OpenWrite($"{args[0]}.btw"),
                Encoding.ASCII.GetBytes(args[1])
            );
        }
    }
}
