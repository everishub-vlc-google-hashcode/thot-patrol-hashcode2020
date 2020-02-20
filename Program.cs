using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HashCode2020
{
    class Program
    {
        static File FileIn;
        static File FileOut;

        static async Task Main(string[] args)
        {
            if (args.Length > 1)
            {
                FileIn  = new File(args[0]);
                FileOut = new File(args[1]);

                // Do things ...

                var content = await FileIn.ReadLineAsync();
            }
            else
            {
                string exe = string.Empty;
                using (var process = System.Diagnostics.Process.GetCurrentProcess())
                    exe = process.MainModule.FileName;
                Console.WriteLine(string.Empty);
                Console.WriteLine($"usage:");
                Console.WriteLine($" \"{Path.GetFileNameWithoutExtension(exe)}[{Path.GetExtension(exe)}]\" <input_file> <output_file>");
                Console.WriteLine(string.Empty);
                Environment.Exit(0);
            }
        }
    }
}
