using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace HashCode2020
{
    public class FileRead
    {
        private StreamReader Reader;

        public FileRead(string fileName)
        {
            this.Reader = new StreamReader(fileName);
        }


        public async Task<string> ReadLineAsync()
        {
            return await Reader.ReadLineAsync();
        }


        public void Close()
        {
            Reader.Close();
            Reader.Dispose();
            Reader = null;
        }


    }
   

}
