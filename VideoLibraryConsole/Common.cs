using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoLibraryConsole
{
	public static class Common
	{
		public static string SetDefaultIfEmpty(string valueToCheck, string defaultValue)
		{
			if (valueToCheck == "")
			{
				return defaultValue;
			} else
			{
				return valueToCheck;
			}

		}


		public static int SetDefaultIfEmptyInt(string valueToCheck, int defaultValue)
		{
			int result;

			if (valueToCheck == "" || int.TryParse(valueToCheck, out result) == false)
			{
				return defaultValue;
			}
			else
			{
				return result;
			}

		}


		public static List<string> GetDirectories(string directoryPath)
		{
			List<string> directories;
			List<string> additionaDirectories = new List<string>();
			string[] files;

			// Get initial list of directories
			directories = Directory.GetDirectories(directoryPath).ToList();
			directories.Sort();

			GetFiles(directoryPath);

			// Go one level deeper (recurrency)
			foreach (var directory in directories)
			{
				additionaDirectories.AddRange(GetDirectories(directory));
			}

			directories.AddRange(additionaDirectories);

			// Get files in direcotries
			files = GetFiles(directoryPath);

			return directories;
		}


		public static string[] GetFiles(string directoryPath)
		{
			return Directory.GetFiles(directoryPath);
		}


		public static List<String> GetVideoFiles(List<string> files, string[] videoFileExtensions)
		{
			string fileExtension;
			List<String> videoFiles = new List<string>();

			foreach (var file in files)
			{
				fileExtension = GetFileExtension(file);

				if (videoFileExtensions.Contains(fileExtension))
				{
					videoFiles.Add(file);
				}
			}

			return videoFiles;
		}


		public static string GetFileExtension(string filename)
		{
			return Path.GetExtension(filename).ToLower();
		}
	}
}
