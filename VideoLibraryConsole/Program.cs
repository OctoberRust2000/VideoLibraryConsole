using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoLibraryConsole
{
    class Program
    {

        static int directoryCounter = 0;
        static int filesCounter = 0;
        static List<String> fileExtensions = new List<String>();
        static List<String> fileExtensionsDistinct = new List<String>();
        static string[] videoFileExtensions = {".webm", ".mkv", ".flv", ".flv", ".vob", ".ogv", ".ogg", ".drc", ".mng", ".avi", ".mov", ".qt", ".wmv", ".yuv", ".rm", ".rmvb", ".asf", ".amv", ".mp4", ".m4p", ".m4v", ".mpg", ".mp2", ".mpeg", ".mpe", ".mpv", ".mpg", ".mpeg", ".m2v", ".m4v", ".svi", ".3gp", ".3g2", ".mxf", ".roq", ".nsv", ".flv", ".f4v", ".f4p", ".f4a", ".f4b"};
        static List<String> videoFiles = new List<String>();

		static void Main(string[] args)
        {
            
            string directoryPath = "X:\\";

            //GetDirectories(directoryPath);

            GetFiles(directoryPath);

            fileExtensionsDistinct = fileExtensions.Distinct().ToList();

            Console.WriteLine("Directories number: " + directoryCounter);
            Console.WriteLine("Files number: " + filesCounter);

            Console.WriteLine("File extensions:");
            foreach (var fileExtension in fileExtensionsDistinct)
            {
                Console.WriteLine(fileExtension);
            }

			Console.WriteLine("Video files:");
			foreach (var videoFile in videoFiles)
			{
				Console.WriteLine(videoFile);
			}

			Console.ReadKey();
        }


        static void GetDirectories(string directoryPath)
        {
            string[] directories;

            directories = Directory.GetDirectories(directoryPath);
            GetFiles(directoryPath);

            for (int i = 0; i < directories.Length; i++)
            {
                directoryCounter++;
                //Console.WriteLine(directories[i]);  
                GetDirectories(directories[i]);
            }
        }


        static void GetFiles(string directoryPath)
        {
            string[] files;
            string fileExtension;

            files = Directory.GetFiles(directoryPath);

            for (int i = 0; i < files.Length; i++)
            {
                filesCounter++;
                //Console.WriteLine(files[i]);
                fileExtension = GetFileExtension(files[i]);
                fileExtensions.Add(fileExtension);
				
				if (videoFileExtensions.Contains(fileExtension))
				{
					videoFiles.Add(files[i]);
				}
            }
        }


        static string GetFileExtension(string filename)
        {
            return Path.GetExtension(filename).ToLower();
        }
    }
}
