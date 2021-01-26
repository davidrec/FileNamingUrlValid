using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace FileNamingUrlValid
{
    class Program
    {
        public static bool isDebug = false;
        static void Main(string[] args)
        {
            SetDebugMode();

            FileInfo[] files = GetDirFilesList();

            List<FileInfo> FilesListToTupdateName = GetFilesListToUpdate(files);

            CopyFilesToValidName(FilesListToTupdateName);

            Console.ReadLine();
        }

        private static void CopyFilesToValidName(List<FileInfo> filesListToTupdateName)
        {
            System.IO.StreamWriter logFilesCopied = new System.IO.StreamWriter(filesListToTupdateName[0].Directory+"\\Files_Copied.txt");
            System.IO.StreamWriter SqlUpdateStaments = new System.IO.StreamWriter(filesListToTupdateName[0].Directory + "\\UpdateFileNames.sql");

            foreach (FileInfo file in filesListToTupdateName)
            {
                string newName = file.Name.Replace(' ', '-');
                string fileFullPath = file.Directory + "\\" + newName;
                PrintDebug("Copy \"" + file.Name + "\" to: \"" + newName + "\"");
                file.CopyTo(fileFullPath, false);
                logFilesCopied.WriteLine(fileFullPath);
                SqlUpdateStaments.WriteLine("UPDATE StoreItems SET ItemImage = '"+ newName + "' WHERE ItemImage = '" + file.Name + "';");
            }
            logFilesCopied.Close();
            SqlUpdateStaments.Close();
        }

        private static List<FileInfo> GetFilesListToUpdate(FileInfo[]  files)
        {
            List<FileInfo>  FilesListToTupdateName = new List<FileInfo>();
            System.IO.StreamWriter logFilesWithSpaces = new System.IO.StreamWriter(files[0].Directory + "\\Files_With_Spaces.txt");
            
            foreach (FileInfo item in files)
            {
                if (item.Name.Contains(" "))
                {
                    FilesListToTupdateName.Add(item);
                    logFilesWithSpaces.WriteLine(item.Name);
                    PrintDebug(item.Name);
                }
            }
            logFilesWithSpaces.Close();
            return FilesListToTupdateName;
        }

        private static FileInfo[] GetDirFilesList()
        {
            Console.WriteLine("Please Enter Path to Image directry");
            var dirPath = @"" + Console.ReadLine();


            if (!Directory.Exists(dirPath))
            {
                exit("Directry not exist!");
            }
            var path = new DirectoryInfo(dirPath);
            return path.GetFiles();
        }

        private static void PrintDebug(string str)
        {
           if(isDebug)
            {
                Console.WriteLine(str);
            }
        }

        private static void SetDebugMode()
        {
            Console.WriteLine("Debug mode? (y/n)");
            string isDebugStr = Console.ReadLine();
            if (isDebugStr == "y")
            {
                isDebug = true;
            }
        }

        private static void exit(string err)
        {
            Console.WriteLine(err);
            Console.ReadLine();
            return;
        }
    }
}
