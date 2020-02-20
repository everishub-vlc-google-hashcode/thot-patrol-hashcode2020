using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HashCode2020
{
    static class WriteResult
    {
        /// <summary>
        /// name: filename
        /// 
        /// data: cada entrada en la matriz es una libreria, cada libreria se define como:
        /// 
        /// int 0: library id to do the signup process
        /// int 1: books for scanning
        /// int 2-X: books of the library 
        /// 
        /// </summary>
        /// <param name="libraries"></param>
        /// <param name="data"></param>
        public static async Task WriteResultAsync(string name, int libraries, int[][] data)
        {
            using (StreamWriter writer = System.IO.File.CreateText(name))
            {
                await writer.WriteAsync(libraries.ToString());
                await writer.WriteAsync('\n');

                for (int i = 0; i < data.Length; i++)
                {
                    await writer.WriteAsync(data[i][0].ToString());
                    await writer.WriteAsync(' ');
                    await writer.WriteAsync(data[i][1].ToString());
                    await writer.WriteAsync('\n');

                    for (int x = 2; x < data[i].Length; x++)
                    {
                        await writer.WriteAsync(data[i][x].ToString());
                        if (x < (data[i].Length-1)) await writer.WriteAsync(' ');
                    }
                    if (i < data.Length -1) await writer.WriteAsync('\n');
                }
            }
        }
    }
}
