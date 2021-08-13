using Leviathan.Services;
using Leviathan.WebApi;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;


namespace TheLeviathan.FileDataSystem {

	public interface IFileSystemService {
		string LocalDirectory { get; }
		IEnumerable<string> LocalFiles(string searchPattern = "*");
	}

	[SingletonService(typeof(IFileSystemService))]
	public class FileSystemService : IFileSystemService {

		public FileSystemService() { }

		public string LocalDirectory =>
			Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

		public IEnumerable<string> LocalFiles(string searchPattern = "*") =>
			Directory.GetFiles(LocalDirectory, searchPattern);
	}

	[ApiComponent("system")]
	public class FileSystemController : ControllerBase {

		IFileSystemService _service;

		public FileSystemController(IFileSystemService service) => _service = service;

		[HttpGet]
		public IEnumerable<string> LocalFiles(string searchPattern = "*") =>
			Directory.GetFiles(_service.LocalDirectory, searchPattern);
	}
}