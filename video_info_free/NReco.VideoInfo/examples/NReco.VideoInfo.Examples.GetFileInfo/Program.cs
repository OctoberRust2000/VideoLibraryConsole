using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NReco.VideoInfo;

namespace NReco.VideoInfo.Examples.GetFileInfo {
	class Program {
		static void Main(string[] args) {

			var filePath = args.Length>0 ? args[0] : "gizmo.mp4";

			var ffProbe = new FFProbe();
			var videoInfo = ffProbe.GetMediaInfo(filePath);

			Console.WriteLine("Media information for: {0}", filePath);
			Console.WriteLine("File format: {0}", videoInfo.FormatName);
			Console.WriteLine("Duration: {0}", videoInfo.Duration);
			foreach (var tag in videoInfo.FormatTags) {
				Console.WriteLine("\t{0}: {1}", tag.Key, tag.Value);
			}

			foreach (var stream in videoInfo.Streams) {
				Console.WriteLine("Stream {0} ({1})", stream.CodecName, stream.CodecType);
				if (stream.CodecType == "video") {
					Console.WriteLine("\tFrame size: {0}x{1}", stream.Width, stream.Height);
					Console.WriteLine("\tFrame rate: {0:0.##}", stream.FrameRate);
				}
				foreach (var tag in stream.Tags) {
					Console.WriteLine("\t{0}: {1}", tag.Key, tag.Value);
				}
			}

			Console.WriteLine("\nPress any key to exit...");
			Console.ReadKey();
		}
	}
}
