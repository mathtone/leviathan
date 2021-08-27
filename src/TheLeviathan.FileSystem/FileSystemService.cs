using Leviathan.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace TheLeviathan.FileSystem {

	public interface IFileSystemService {
		string LocalDirectory { get; }
		IEnumerable<string> LocalFiles(string searchPattern = "*");
		Stream Open(string path, FileMode mode, FileAccess access, FileShare share);
	}

	[SingletonService(typeof(IFileSystemService))]
	public class FileSystemService : IFileSystemService {

		public FileSystemService() { }

		public string LocalDirectory =>
			Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

		public IEnumerable<string> LocalFiles(string searchPattern = "*") =>
			Directory.GetFiles(LocalDirectory, searchPattern);

		public Stream Open(string path, FileMode mode, FileAccess access, FileShare share) =>
			File.Open(LocalDirectory + "\\" + path, mode, access, share);
	}
}