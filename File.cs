using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

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


        public string ReadLine()
        {
            return ReadLine(lineNumber++);
        }


        public string ReadLine(int lineNumber)
        {
            string line;
            int ptr = 0;
            using (StreamReader file = new StreamReader(fileName))
            {
                while ((line = file.ReadLine()) != null)
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


        public void WriteLine(string line)
        {
            using (StreamWriter file = new StreamWriter(fileName, true))
            {
                file.WriteLine(line);
                file.Flush();
            }
        }



    }
}
