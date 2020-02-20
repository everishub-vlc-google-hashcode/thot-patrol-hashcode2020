using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace HashCode2020
{
    public class File
    {
        private string fileName;
        private int lineNumber;
        public File(string fileName)
        {
            this.fileName = fileName;
            this.lineNumber = 0;
        }


        public async Task<string> ReadLineAsync()
        {
            return await ReadLineAsync(lineNumber++);
        }


        public async Task<string> ReadLineAsync(int lineNumber)
        {
            string line;
            int ptr = 0;
            using (StreamReader file = new StreamReader(fileName))
            {
                while ((line = await file.ReadLineAsync()) != null)
                {
                    if (lineNumber == ptr)
                        break;
                    ptr++;
                }
                file.Close();
            };
            return ptr > lineNumber ? null : line;
        }



        public void Reset()
        {
            lineNumber = 0;
        }


        public async Task WriteLineAsync(string line)
        {
            using (StreamWriter file = new StreamWriter(fileName, true))
            {
                await file.WriteLineAsync(line);
                await file.FlushAsync();
            }
        }



    }
}
