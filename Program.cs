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
            public int Id { get; set; }

            public int BookCount { get; set; }
            public int SignUpTime { get; set; }
            public int BooksPerDay { get; set; }
            public int[] Books { get; set; }
            public int TotalBookScore { get; set; }

            public int LibraryScore { get; set; }
        }



        static async Task Main(string[] args)
        {
#if !DEBUG
            if (args.Length > 1)
            {
                FileIn = new FileRead(args[0]);
//                FileOut = new File(args[1]);
#else
            FileIn = new FileRead(@"..\..\..\b_read_on.txt");
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

            // Order libraries by libraryScore
            LibraryList = LibraryList.OrderByDescending(x => x.LibraryScore).ToArray();

            var librariesToRead = 0;

            var totalSignUpTime = 0;

            foreach (var item in LibraryList)
            {
                totalSignUpTime += item.SignUpTime;

                if (totalSignUpTime < DaysForScanning) librariesToRead++;
            }


            Console.WriteLine("We can read" + librariesToRead + "before running out of time");



            var libsToSend = new int[librariesToRead][];

            for (int x = 0; x < librariesToRead; x++)
            {
                var libArray = new int[LibraryList[x].BookCount + 2];

                libArray[0] = LibraryList[x].Id;
                libArray[1] = LibraryList[x].BookCount;

                var bookPointer = 0;

                for (int i = 2; i < libArray.Length; i++)
                {
                    libArray[i] = LibraryList[x].Books[bookPointer];
                    bookPointer++;
                }


                libsToSend[x] = libArray;
            }

            await WriteResult.WriteResultAsync("output_b.txt", librariesToRead, libsToSend);

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

                lib.Id = f;

                lib.BookCount   = libHEader[0];
                lib.SignUpTime  = libHEader[1];
                lib.BooksPerDay = libHEader[2];


                line = await FileIn.ReadLineAsync();
                lib.Books = line.Split(' ').Select(x => int.Parse(x)).ToArray();
                lib.TotalBookScore = lib.Books.Sum(x => Scores[x]);

                lib.LibraryScore = lib.TotalBookScore;

                LibraryList[f] = lib;
            }
        }


    }



}
