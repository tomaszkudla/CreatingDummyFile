using System;
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
                ParseParams(args, out string path, out int sizeMB);
                if (sizeMB <= 0)
                {
                    throw new ArgumentException("File size should be integer > 0 MB");
                }

                GenerateFileWithRandom(path, sizeMB);
                Console.WriteLine("Done");
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
            var count = (sizeMB * 1024 * 1024);
            using (var streamWriter = new StreamWriter(path))
            {
                Random random = new Random();
                for (int i = 0; i < count; i++)
                {
                    var num = random.Next(0, 127);
                    var ch = (char)num;
                    streamWriter.Write(ch);
                }
            }
        }
    }
}
