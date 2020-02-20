using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HashCode2020
{
    class Program
    {
        static FileRead FileIn;


        static int Books;
        static int Libraries;
        static int DaysForScanning;

        public class Library
        {
            public int BookCount { get; set; }
            public int SignUpTime { get; set; }
            public int BooksPerDay { get; set; }
            public int[] Books { get; set; }
            public int TotalBookScore { get; set; }
        }



        static async Task Main(string[] args)
        {
#if !DEBUG
            if (args.Length > 1)
            {
                FileIn = new FileRead(args[0]);
//                FileOut = new File(args[1]);
#else
            FileIn = new File(@"c:\dev\thot-patrol-hashcode2020\bin\Release\netcoreapp3.1\c_incunabula.txt");
 //           FileOut = new File ("f_libraries_of_the_world.out");
#endif

                await Read();
                Console.WriteLine($"Libraries: {Libraries} - Books: {Books} - DaysForScanning: {DaysForScanning}");

                int l = 0;
                foreach (Library b in LibraryList)
                {
                    Console.WriteLine($"[{l++}]  SignUpTime {b.SignUpTime} -  BookCount: {b.BookCount} - BooksPerDay: {b.BooksPerDay}");
                }

                FileIn.Close();
#if !DEBUG

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
#endif
        }


        static int[] Scores;
        static Library[] LibraryList;

        static async Task Read()
        {
            // List<int> Scores = new List<int>();

            string line = await FileIn.ReadLineAsync();

            // Header
            string[] Header = line.Split(' ');
            Books = int.Parse(Header[0]);
            Libraries = int.Parse(Header[1]);
            DaysForScanning = int.Parse(Header[2]);

            line = await FileIn.ReadLineAsync();
            Scores = line.Split(' ').Select(x => int.Parse(x)).ToArray();

            LibraryList = new Library[Libraries];

            for (int f = 0; f < Libraries; f++)
            {
                Library lib = new Library();

                line = await FileIn.ReadLineAsync();
                var libHEader   = line.Split(' ').Select(x => int.Parse(x)).ToArray();
                lib.BookCount   = libHEader[0];
                lib.SignUpTime  = libHEader[1];
                lib.BooksPerDay = libHEader[2];


                line = await FileIn.ReadLineAsync();
                lib.Books = line.Split(' ').Select(x => int.Parse(x)).ToArray();
                lib.TotalBookScore = lib.Books.Sum(x => Scores[x]);
                LibraryList[f] = lib;
            }
        }


    }



}
