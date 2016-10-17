using System;
using System.IO;

namespace Miroslav_project
{
    public class Commandss
    {
        public void exeptionMistakes(Exception e)
        {
            Console.WriteLine(e.Message);
            Console.Write("\n{0}> ", Directory.GetCurrentDirectory());
        }

        public void  wrongArgs(string input) 
        {
            Console.WriteLine("'{0}'is not recognized as an internal or external command", input);
            Console.Write("\n{0}> ", Directory.GetCurrentDirectory());
        }
        
        public void mistakes(string splits , int numberOfArguments)
        {
            if (numberOfArguments == 1)
            {
                Console.WriteLine("You should enter 1 more string to call command {0},type help for more info !", splits);
                Console.Write("\n{0}> ", Directory.GetCurrentDirectory());
            }
            else if (numberOfArguments > 1)
            {
                Console.WriteLine("You should enter 2 more strings to call command {0},type help for more info", splits);
                Console.Write("\n{0}> ", Directory.GetCurrentDirectory());
            }
        }
     
        public void HelpComandsFromFile(string splits,string AppDir)
        {
            
            string text = File.ReadAllText(Directory.GetParent(AppDir).ToString() + "\\HelpFiles\\" + splits + ".txt");
            Console.WriteLine(text);

            string home2 = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile); 
            Directory.SetCurrentDirectory(home2);
            Console.Write("\n{0}> ", Directory.GetCurrentDirectory());
        }

        public void commands(string AppDir) 
        {
            WalkingDirectory di = new WalkingDirectory(); 
            for (; true;)
            {

                string input = Console.ReadLine();
                string[] split = input.Split(' ');

                //CD

                if (split.Length == 1 && split[0] == "cd")
                {
                    Console.Write("\n{0}> ", Directory.GetCurrentDirectory());
                }
                try
                {
                    if (split.Length == 2 && split[0] == "cd" && split[1] == "..")
                    {
                        string mr = Directory.GetParent(Directory.GetCurrentDirectory()).ToString();
                        Directory.SetCurrentDirectory(mr);
                        Console.Write("\n{0}> ", Directory.GetCurrentDirectory());
                    }
                }
                catch (NullReferenceException e) { exeptionMistakes(e); }
                
                try
                {
                    if (split.Length == 2 && split[0] == "cd" && split[1] != "..")
                    {
                        Directory.SetCurrentDirectory(@split[1]);
                        Console.Write("\n{0}> ", Directory.GetCurrentDirectory());
                    }
                }
                catch (DirectoryNotFoundException e)    { exeptionMistakes(e);   }
                catch (UnauthorizedAccessException e)   { exeptionMistakes(e);   }
             
                    if (split.Length > 2 && split[0] == "cd" && split[1] != "..") { wrongArgs(input); }
                    if (split.Length > 2 && split[0] == "cd" && split[1] == "..") { wrongArgs(input); }
                    
                //CD

                //CLR
                if (split.Length == 1 && split[0] == "clr")
                {
                    Console.Clear();
                    Console.Write("\n{0}> ", Directory.GetCurrentDirectory());
                }
                if (split.Length > 1 && split[0] == "clr") { wrongArgs(input); }
                //CLR

                //COPY
                if (split.Length == 3 && split[0] == "copy")
                {
                    string sourceFile = @split[1];
                    string destinationDirectory = @split[2];
                    string destFile =Path.Combine(destinationDirectory,sourceFile);
                    try
                    {
                        if (!Directory.Exists(destinationDirectory))
                        {
                            Directory.CreateDirectory(destinationDirectory);
                        }
                        File.Copy(sourceFile, destFile);
                        Console.WriteLine("Succesfully copied.");
                        Console.Write("\n{0}> ",Directory.GetCurrentDirectory());
                    }
                    catch (FileNotFoundException e)              { exeptionMistakes(e);    }
                    catch (IOException e)                        { exeptionMistakes(e);    }
                    catch (UnauthorizedAccessException e)        { exeptionMistakes(e);    }
                }
                if (split.Length == 2 && split[0] == "copy")     {  mistakes(split[0],1);  }
                if (split.Length == 1 && split[0] == "copy")     {  mistakes(split[0],2);  }
                if (split.Length > 3 && split[0] == "copy")      {  wrongArgs(input);      } 

                
                //COPY

                //DEL
                if (split.Length == 2 && split[0] == "del")
                {
                    if (File.Exists(@split[1]))
                    {
                        try
                        {
                            File.Delete(@split[1]);
                            Console.Write("Successfully deleted \t" + @split[1] + "\t" + Directory.GetCurrentDirectory() + "> ");
                        
                        }
                        catch (IOException e)  { exeptionMistakes(e); }
                    }
                    else
                    {
                        Console.WriteLine(@split[1] + " does not exists \t");
                        Console.Write(Directory.GetCurrentDirectory() + "> ");
                    }
                }
                if (split.Length == 1 && split[0] == "del")   {  mistakes(split[0],1); }
                if (split.Length > 2 && split[0] == "del") { wrongArgs(input); } 
                 //DEL
                 
                //DIR
                if (split.Length == 1 && split[0] == "dir")
                {
                    di.DirSearch(Directory.GetCurrentDirectory());
                }
                if (split.Length == 2 && split[0] == "dir")
                {
                    di.DirSearch(@split[1]);
                }
                if (split.Length > 2 && split[0] == "dir") { wrongArgs(input); } 
                //DIR

                //ECHO
                if (split[0] == "echo")
                {
                    for (int i = 1; i < split.Length; i++)
                    {
                        Console.Write(split[i] + ' ');
                    }
                    Console.Write("\n{0}> ", Directory.GetCurrentDirectory());
                }
                if (split.Length == 1 && split[0] == "echo")  { mistakes(split[0],1);  }
                //ECHO


                // HELP
                if (split.Length == 1 && split[0] == "help")                            {  HelpComandsFromFile(split[0], AppDir);    }

                if (split.Length == 2 && split[0] == "help" && split[1] == "cd")        {  HelpComandsFromFile(split[1], AppDir);    }
                if (split.Length == 2 && split[0] == "help" && split[1] == "clr")       {  HelpComandsFromFile(split[1], AppDir);    }
                if (split.Length == 2 && split[0] == "help" && split[1] == "copy")      {  HelpComandsFromFile(split[1], AppDir);    }
                if (split.Length == 2 && split[0] == "help" && split[1] == "del")       {  HelpComandsFromFile(split[1], AppDir);    }
                if (split.Length == 2 && split[0] == "help" && split[1] == "dir")       {  HelpComandsFromFile(split[1], AppDir);    }
                if (split.Length == 2 && split[0] == "help" && split[1] == "echo")      {  HelpComandsFromFile(split[1], AppDir);    }
                if (split.Length == 2 && split[0] == "help" && split[1] == "mkdir")     {  HelpComandsFromFile(split[1], AppDir);    }
                if (split.Length == 2 && split[0] == "help" && split[1] == "move")      {  HelpComandsFromFile(split[1], AppDir);    }
                if (split.Length == 2 && split[0] == "help" && split[1] == "quit")      {  HelpComandsFromFile(split[1], AppDir);    }
                if (split.Length == 2 && split[0] == "help" && split[1] == "rmdir")     {  HelpComandsFromFile(split[1], AppDir);    }
                if (split.Length == 2 && split[0] == "help" && split[1] == "time")      {  HelpComandsFromFile(split[1], AppDir);    }

                if (split.Length > 2 && split[0] == "help" && split[1] == "cd")     { wrongArgs(input); }
                if (split.Length > 2 && split[0] == "help" && split[1] == "clr")    { wrongArgs(input); }
                if (split.Length > 2 && split[0] == "help" && split[1] == "copy")   { wrongArgs(input); }
                if (split.Length > 2 && split[0] == "help" && split[1] == "del")    { wrongArgs(input); }
                if (split.Length > 2 && split[0] == "help" && split[1] == "dir")    { wrongArgs(input); }
                if (split.Length > 2 && split[0] == "help" && split[1] == "echo")   { wrongArgs(input); }
                if (split.Length > 2 && split[0] == "help" && split[1] == "mkdir")  { wrongArgs(input); }
                if (split.Length > 2 && split[0] == "help" && split[1] == "move")   { wrongArgs(input); }
                if (split.Length > 2 && split[0] == "help" && split[1] == "quit")   { wrongArgs(input); }
                if (split.Length > 2 && split[0] == "help" && split[1] == "rmdir")  { wrongArgs(input); }
                if (split.Length > 2 && split[0] == "help" && split[1] == "time")   { wrongArgs(input); }

                //HELP

                //MKDIR
                if (split.Length == 2 && split[0] == "mkdir")
                {
                    try
                    {
                        string folderName = Directory.GetCurrentDirectory();

                        string pathString = Path.Combine(folderName, split[1]);

                        if (!Directory.Exists(pathString))
                        {
                            Directory.CreateDirectory(pathString); 
                            string fileName = Path.GetRandomFileName(); 
                            pathString = Path.Combine(pathString, fileName);
                            Console.WriteLine("Succesfully created directory: ");
                            Console.Write("{0}> ", Directory.GetCurrentDirectory());
                        }
                        else
                        {
                            Console.WriteLine("Directory {0} already exists: ", pathString);
                            Console.Write("{0}> ",Directory.GetCurrentDirectory());
                        }
                    }
                    catch (IOException e) { exeptionMistakes(e); }
                }
                if (split.Length == 1 && split[0] == "mkdir")   {  mistakes(split[0],1); }
                if (split.Length > 2 && split[0] == "mkdir") { wrongArgs(input); } 
                    //MKDIR

                    //MOVE
                    if (split.Length == 3 && split[0] == "move")
                {
                    string sourceFile = @split[1];
                    string destinationFile = @split[2];
                    string destFile = destinationFile + "\\" + sourceFile;
                    try
                    {
                        File.Copy(sourceFile, destFile);
                        Console.WriteLine("Successfully moved {0} to {1}" , sourceFile, destinationFile);
                        Console.Write("{0}> ",Directory.GetCurrentDirectory());
                    }
                    catch (IOException e) { exeptionMistakes(e); }
                    
                }
                if (split.Length == 2 && split[0] == "move")  { mistakes(split[0],1); }
                if (split.Length == 1 && split[0] == "move") { mistakes(split[0],2); }
                if (split.Length > 3 && split[0] == "move") { wrongArgs(input); }
                    //MOVE

                    //QUIT
                    if (split.Length == 1 && split[0] == "quit")
                {
                    break;
                }
                if (split.Length > 1 && split[0] == "quit") { wrongArgs(input); }
                    //QUIT


                    //RMDIR
                    if (split.Length == 2 && split[0] == "rmdir")
                    {
                        if (Directory.Exists(@split[1]))
                        {
                            try
                            {
                                Directory.Delete(@split[1]);
                                Console.WriteLine("Successfully deleted {0}", split[1]);
                            }
                            catch (IOException e) {   exeptionMistakes(e);  }
                        }
                        Console.Write("\n{0}> ", Directory.GetCurrentDirectory());
                    }
                if (split.Length == 1 && split[0] == "rmdir") {   mistakes(split[0],1);  }
                if (split.Length > 2 && split[0] == "rmdir") { wrongArgs(input); }
                    //RMDIR

                 //TIME
                    if (split.Length == 1 && split[0] == "time")
                {
                    DateTime now = DateTime.Now;
                    Console.Write("The current time and date : ");
                    Console.WriteLine(now);
                    Console.Write("\n{0}> ", Directory.GetCurrentDirectory());
                }
                if (split.Length > 1 && split[0] == "time") { wrongArgs(input); }
                //TIME
                
                //ELSE
                else if (split[0] != "cd" && split[0] != "clr" && split[0] != "copy" && split[0] != "del" && split[0] != "dir" && split[0] != "echo" && split[0] != "help" && split[0] != "mkdir" && split[0] != "move" && split[0] != "quit" && split[0] != "rmdir" && split[0] != "time")
                {
                    wrongArgs(input);
                 
                }

                //ELSE            
            }
        }

    }

   
}
