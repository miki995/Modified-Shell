using System;
using System.IO;

namespace Miroslav_project
{
    public class WalkingDirectory
    {
        Commandss comi = new Commandss();
        
        public void DirSearch(string sDir) 
        {
            try
            {
                int numberOfDirectory = 0;
                foreach (string directory in Directory.GetDirectories(sDir))
                {

                    DateTime dr = Directory.GetLastWriteTime(Directory.GetCurrentDirectory());
                    Console.WriteLine("{0} \t <dir> \t {1} \n", dr, Path.GetFileName(directory));
                    numberOfDirectory++;
                }
                long SizeOfFiles = 0;
                int numberOfFiles = 0;
                foreach (string file in Directory.GetFiles(sDir))
                {
                    long size = file.Length;   
                    DateTime dr = Directory.GetLastWriteTime(Directory.GetCurrentDirectory()); 
                    Console.WriteLine("{0}\t {1} bytes \t \t  {2} \n", dr, size, Path.GetFileName(file));
                    SizeOfFiles += size;
                    numberOfFiles++;
                }
                Console.WriteLine("\t \t \t {0} File(s) \t {1} \t bytes", numberOfFiles, SizeOfFiles);
                Console.WriteLine("\t \t \t {0} Dir(s)", numberOfDirectory);
                Console.Write("\n{0}> ", Directory.GetCurrentDirectory());
            }
            catch (Exception e) { comi.exeptionMistakes(e); }
        }
    }
}


