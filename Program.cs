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

                Read();
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


        static int[] VarArray;

        static async void Read()
        {
            List<int> VarList = new List<int>();
            
            string line = await FileIn.ReadLineAsync();

            // Header
            string[] Header = line.Split(' ');

            while ((line = await FileIn.ReadLineAsync()) != null)
            {
                foreach (string s in line.Split(' '))
                    VarList.Add(int.Parse(s));
            }

            VarArray = VarList.ToArray();
            VarList.Clear();
            VarList = null;

        }




    }
}
