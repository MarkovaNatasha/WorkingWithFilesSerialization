using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Task8
{
    public class FoundFile
    {
        public string StrPattern { get; set; }
        public string PathDirectory { get; set; }

        public FoundFile(string strPattern, string pathDirectory)
        {
            StrPattern = strPattern;
            PathDirectory = pathDirectory;
        }

        public string GetFiles()
        {
            try
            {
                FileInfo[] files = new DirectoryInfo(PathDirectory).GetFiles("*.txt");
                Console.WriteLine("All txt files {0}.", files.Length);
                foreach (FileInfo file in files)
                {
                    if (Regex.IsMatch(file.Name, StrPattern))
                    {
                        return string.Format("This file has coincided with a template -> {0}", file.Name);
                    }
                }
            }
            catch(ArgumentException)
            {
                return "Incorrect file path...";
            }
            return "No matches...";
        }
    }
}
