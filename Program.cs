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

        static string[] fileNames =
        {
            "a_example.txt",
            "b_read_on.txt",
            "c_incunabula.txt",
            "d_tough_choices.txt",
            "e_so_many_books.txt",
            "f_libraries_of_the_world.txt"
        };

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
            public List<int> BooksToSend { get; set; } = new List<int>();
            public int TotalBookScore { get; set; }


            public int StartDay { get; set; }

            public int BooksBeforeDeadline { get; set; }

            public int LibraryScore { get; set; }
        }



        static async Task Main(string[] args)
        {
            foreach (var file in fileNames)
            {
                await Avril(file);
            }
        }



        static async Task Avril(string filename)
        {

            FileIn = new FileRead(@$"..\..\..\{filename}");

                await Read();
                Console.WriteLine($"Libraries: {Libraries} - Books: {Books} - DaysForScanning: {DaysForScanning}");

                int l = 0;
                //foreach (Library b in LibraryList)
                //{
                //    //Console.WriteLine($"[{l++}]  SignUpTime {b.SignUpTime} -  BookCount: {b.BookCount} - BooksPerDay: {b.BooksPerDay}");
                //}

                FileIn.Close();


            var alreadySentBooks = new bool[Books];

            // Order libraries by signUpTime
            LibraryList = LibraryList.OrderBy(x => x.SignUpTime).ToArray();

            //Order library books by highest score
            foreach (var lib in LibraryList)
            {
                lib.Books = lib.Books.OrderByDescending(x => Scores[x]).ToArray();
            }

            //compute start days

            var previousDay = 0;

            for (int i = 0; i < LibraryList.Length; i++)
            {
                LibraryList[i].StartDay = previousDay + LibraryList[i].SignUpTime;

                previousDay = LibraryList[i].StartDay;

                LibraryList[i].BooksBeforeDeadline = (DaysForScanning - previousDay) * LibraryList[i].BooksPerDay;
            }


            var librariesToRead = 0;

            var totalSignUpTime = 0;

            foreach (var item in LibraryList)
            {
                totalSignUpTime += item.SignUpTime;

                if (totalSignUpTime < DaysForScanning) librariesToRead++;
            }

            foreach (var lib in LibraryList)
            {
                var addedBooks = 0;
                for (int i = 0; (i < lib.Books.Length) && addedBooks <= lib.BooksBeforeDeadline; i++)
                {
                    if (!alreadySentBooks[lib.Books[i]])
                    {
                        lib.BooksToSend.Add(lib.Books[i]);
                        alreadySentBooks[lib.Books[i]] = true;
                        addedBooks++;
                    }
                }
            }

            Console.WriteLine("We can read " + librariesToRead + " libraries before running out of time");

            var libsToSend = new int[librariesToRead][];

            for (int x = 0; x < librariesToRead; x++)
            {
                if (LibraryList[x].BooksToSend.Count > 0)
                {
                    var libArray = new int[LibraryList[x].BooksToSend.Count + 2];

                    libArray[0] = LibraryList[x].Id;
                    libArray[1] = LibraryList[x].BooksToSend.Count;

                    for (int i = 2; i < libArray.Length; i++)
                    {
                        libArray[i] = LibraryList[x].BooksToSend[i - 2];
                    }

                    libsToSend[x] = libArray;
                }
            }


            libsToSend = libsToSend.Where(x => x != null).ToArray();

            await WriteResult.WriteResultAsync(filename, libsToSend.Length, libsToSend);

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
