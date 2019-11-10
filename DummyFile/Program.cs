using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace DummyFile
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var stopwatch = Stopwatch.StartNew();
                ParseParams(args, out string path, out int sizeMB);
                if (sizeMB <= 0)
                {
                    throw new ArgumentException("File size should be integer > 0 MB");
                }

                GenerateFileWithRandom(path, sizeMB);
                stopwatch.Stop();
                Console.WriteLine($"Done in {stopwatch.ElapsedMilliseconds} ms");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void ParseParams(string[] args, out string path, out int sizeMB)
        {
            path = args.ElementAtOrDefault(0);
            int.TryParse(args.ElementAtOrDefault(1), out sizeMB);
        }

        private static void GenerateFileWithRandom(string path, int sizeMB)
        {
            using (var fileStream = new FileStream(path, FileMode.Create))
            using (var binaryWriter = new BinaryWriter(fileStream))
            {
                Random random = new Random();
                for (int i = 0; i < sizeMB; i++)
                {
                    var part = new byte[1024 * 1024];
                    random.NextBytes(part);
                    binaryWriter.Write(part);
                }
            }
        }
    }
}
