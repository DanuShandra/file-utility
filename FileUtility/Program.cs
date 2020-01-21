using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileUtility
{
    class Program
    {
        // Global variables
        static string filePath;
        static double fileSize = 0;
        static long fileCount = 0;

        // Path validation function
        static bool isValidation()
        {
            // return true if folder exists at end of path
            bool flg = Directory.Exists(filePath);
            return flg;
        }
 
        // Display file Info function 
        static void getFileInfo()
        {
            DirectoryInfo df = new DirectoryInfo(filePath);

            // Get file and directory info
            FileInfo[] fileInfo = df.GetFiles();
            DirectoryInfo[] dirInfo = df.GetDirectories();

            // Display file info
            foreach (FileInfo fi in fileInfo)
            {
                double fSize = fi.Length / 1000.0;
                Console.WriteLine(fi.FullName.PadRight(75) + "\t" + fSize + " KB");
                fileCount++;
                fileSize += fSize;
            }

            // Display directory info
            foreach (DirectoryInfo di in dirInfo)
            {
                getRecursiveFileInfo(di.FullName);
            }
        }

        // Recursive function to search through folders
        static void getRecursiveFileInfo(string dirPath)
        {
            DirectoryInfo df = new DirectoryInfo(dirPath);

            // Get file and directory info
            FileInfo[] fileInfo = df.GetFiles();
            DirectoryInfo[] dirInfo = df.GetDirectories();

            // Display file info
            foreach (FileInfo fi in fileInfo)
            {
                double fSize = fi.Length / 1000.0;
                Console.WriteLine(fi.FullName.PadRight(75) + "\t" + fSize + " KB");
                fileCount++;
                fileSize += fSize;
            }

            // Display directory info
            foreach (DirectoryInfo di in dirInfo)
            {
                getRecursiveFileInfo(di.FullName);
            }
        }


        static void Main(string[] args)
        {
            // Check for zero arguments
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: FileUtility <Path>");
            }
            else
            {
                // File path is first argument
                filePath = args[0];

                // Check for directory path
                if (!isValidation())
                {
                    Console.WriteLine("ERROR: invalid folder path");
                }
                else
                {
                    Console.WriteLine("SUCCESS: " + filePath + " is a valid folder!");

                    try
                    {
                        DateTime strtTime = DateTime.Now;

                        // Get file info
                        getFileInfo();

                        DateTime endTime = DateTime.Now;
                        TimeSpan fileGatherTime = endTime - strtTime;

                        Console.WriteLine();    // line break to seperate summary info
                        
                        // Display total file size
                        if (fileSize > 1000)
                        {
                            Console.WriteLine("Total File Size: " + fileSize/1000 + " MB");
                        }
                        else
                        {
                            Console.WriteLine("Total File Size: " + fileSize + " KB");
                        }
                        
                        // Display number of files found
                        Console.WriteLine("Total Number of Files: " + fileCount);

                        // Display time elapsed
                        Console.WriteLine("Total Time Elapsed: " + fileGatherTime.TotalMilliseconds + " ms");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}
