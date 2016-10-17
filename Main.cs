using System;
using System.IO;
namespace Miroslav_project
{
    class TestProgram
    {
        static void Main(string[] args)
        {
            Console.Title = "Project Miroslav Maksimovic"; 
            Console.ForegroundColor = ConsoleColor.Green; 
            SystemInfo si = new SystemInfo();
            si.getOperatingSystemInfo();          
            si.getProcessorInfo();          

            string AppDir = Directory.GetParent(Directory.GetCurrentDirectory()).ToString();
            string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile); 

            Directory.SetCurrentDirectory(userProfile);
            Console.Write("\n{0}> ", Directory.GetCurrentDirectory());
			
            Commandss comi = new Commandss();
            comi.commands(AppDir);
        }
    }
}