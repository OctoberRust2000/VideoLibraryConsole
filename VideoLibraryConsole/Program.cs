using MediaInfoLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;



namespace VideoLibraryConsole
{
    class Program
    {

        static int directoryCounter = 0;
        static int filesCounter = 0;
        static List<String> fileExtensions = new List<String>();
        static List<String> fileExtensionsDistinct = new List<String>();
        static string[] videoFileExtensions = {".webm", ".mkv", ".flv", ".flv", ".vob", ".ogv", ".ogg", ".drc", ".mng", ".avi", ".mov", ".qt", ".wmv", ".yuv", ".rm", ".rmvb", ".asf", ".amv", ".mp4", ".m4p", ".m4v", ".mpg", ".mp2", ".mpeg", ".mpe", ".mpv", ".mpg", ".mpeg", ".m2v", ".m4v", ".svi", ".3gp", ".3g2", ".mxf", ".roq", ".nsv", ".flv", ".f4v", ".f4p", ".f4a", ".f4b"};
        static List<String> files = new List<String>();
		static List<String> videoFiles = new List<String>();

		static void Main(string[] args)
        {
            
            string directoryPath = "X:\\";
			List<string> directories = new List<string>();
			var channelsNumber = 0;

			VideoLibraryConsoleDataContext VideoConsoleDB = new VideoLibraryConsoleDataContext();

			directories = Common.GetDirectories(directoryPath);
			

			foreach (var directory in directories)
			{
				//Console.WriteLine(directory);
				files.AddRange(Common.GetFiles(directory));
			}

			Console.WriteLine("Directories count: " + directoryCounter);
            Console.WriteLine("Files count: " + filesCounter);

			
			files.Sort();

			videoFiles = Common.GetVideoFiles(files, videoFileExtensions);
			
			/*
			foreach (var videoFile in videoFiles)
			{
				Console.WriteLine(videoFile);
			}
			*/
			
			Console.WriteLine("Video files:");
			foreach (var videoFile in videoFiles)
			{
				var mi = new MediaInfo();
				mi.Open(videoFile);

				var videoInfo = new VideoInfo(mi);
				var audioInfo = new AudioInfo(mi);

				//Console.WriteLine(audioInfo.ChannelPositions);
				//Console.WriteLine(audioInfo.ChannelsOriginal);
				//Console.WriteLine(videoFile);
				//Console.WriteLine(audioInfo.Channels);


				channelsNumber = audioInfo.ChannelsOriginal > audioInfo.Channels ? audioInfo.ChannelsOriginal : audioInfo.Channels;

				Console.WriteLine(videoFile);


				var existingVideos = from c in VideoConsoleDB.Videos
					where c.FullFileName == videoFile
					select c;

				if (existingVideos.Count() == 0)
				{
					Videos newVideo = new Videos()
					{
						FullFileName = videoFile,
						AudioChannels = channelsNumber,
						FileName = Path.GetFileName(videoFile),
						FilePath = Path.GetDirectoryName(videoFile)
					};

					VideoConsoleDB.Videos.InsertOnSubmit(newVideo);
					// executes the commands to implement the changes to the database
					VideoConsoleDB.SubmitChanges();
				}


				mi.Close();

			}

			
			Console.WriteLine("\nPress any key to exit...");
			Console.ReadKey();
        }


		
    }

	public class VideoInfo
	{
		public string Codec { get; private set; }
		public int Width { get; private set; }
		public int Heigth { get; private set; }
		public double FrameRate { get; private set; }
		public string FrameRateMode { get; private set; }
		public string ScanType { get; private set; }
		public TimeSpan Duration { get; private set; }
		public int Bitrate { get; private set; }
		public string AspectRatioMode { get; private set; }
		public double AspectRatio { get; private set; }

		public VideoInfo(MediaInfo mi)
		{
			Codec = mi.Get(StreamKind.Video, 0, "Format");
			/*
			Width = int.Parse(mi.Get(StreamKind.Video, 0, "Width"));
			Heigth = int.Parse(mi.Get(StreamKind.Video, 0, "Height"));
			Duration = TimeSpan.FromMilliseconds(int.Parse(mi.Get(StreamKind.Video, 0, "Duration")));
			Bitrate = int.Parse(mi.Get(StreamKind.Video, 0, "BitRate"));
			AspectRatioMode = mi.Get(StreamKind.Video, 0, "AspectRatio/String"); //as formatted string
			AspectRatio = double.Parse(mi.Get(StreamKind.Video, 0, "AspectRatio"));
			FrameRate = double.Parse(mi.Get(StreamKind.Video, 0, "FrameRate"));
			FrameRateMode = mi.Get(StreamKind.Video, 0, "FrameRate_Mode");
			ScanType = mi.Get(StreamKind.Video, 0, "ScanType");
			*/
			}
		}

	public class AudioInfo
	{
		public string Codec { get; private set; }
		public string CompressionMode { get; private set; }
		public string ChannelPositions { get; private set; }
		public int Channels { get; private set; }
		public int ChannelsOriginal { get; private set; }
		//public string StreamKind { get; private set; }
		//public TimeSpan Duration { get; private set; }
		public String Duration { get; private set; }
		public int Bitrate { get; private set; }
		public string BitrateMode { get; private set; }
		public int SamplingRate { get; private set; }

		public AudioInfo(MediaInfo mi)
		{
			Codec = mi.Get(StreamKind.Audio, 0, "Format");
			Duration = mi.Get(StreamKind.Audio, 0, "Duration");
			ChannelPositions = mi.Get(StreamKind.Audio, 0, "ChannelPositions");
			Channels = Common.SetDefaultIfEmptyInt(mi.Get(StreamKind.Audio, 0, "Channels"), 0);
			ChannelsOriginal = Common.SetDefaultIfEmptyInt(mi.Get(StreamKind.Audio, 0, "Channel(s)_Original"), 0);
			//StreamKind = mi.Get(StreamKind.Audio, 0, "StreamKind");
			/*Duration = TimeSpan.FromMilliseconds(int.Parse(mi.Get(StreamKind.Audio, 0, "Duration")));
			Bitrate = int.Parse(mi.Get(StreamKind.Audio, 0, "BitRate"));
			BitrateMode = mi.Get(StreamKind.Audio, 0, "BitRate_Mode");
			CompressionMode = mi.Get(StreamKind.Audio, 0, "Compression_Mode");
			
			SamplingRate = int.Parse(mi.Get(StreamKind.Audio, 0, "SamplingRate"));
			*/
		}
	}
}
