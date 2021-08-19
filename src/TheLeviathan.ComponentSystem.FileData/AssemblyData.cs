using Leviathan.Common;
using Leviathan.Components.Sdk;
using Leviathan.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TheLeviathan.ComponentSystem.Data;
using TheLeviathan.FileDataSystem;

namespace TheLeviathan.ComponentSystem.FileData {
	


	[SingletonService(typeof(IAssemblyData))]
	public class AssemblyData : IAssemblyData {
		object updatelock = new object();
		IFileSystemService _fileSystem;
		int? _maxId;

		Dictionary<int, AssemblyRecord> _records;
		protected IDictionary<int, AssemblyRecord> Records => _records ??= LoadAsync().Result.ToDictionary(r => r.Id);
		protected int MaxId => Records.Any() ? Records.Values.Max(r => r.Id) : 0;

		public AssemblyData(IFileSystemService fileSystem) {
			_fileSystem = fileSystem;
			if (!Directory.Exists(_fileSystem.LocalDirectory + "\\Data")) {
				Directory.CreateDirectory(_fileSystem.LocalDirectory + "\\Data");
			}
		}


		public async Task<int> CreateAsync(AssemblyRecord item) {
			var id = item.Id;
			lock (updatelock) {
				id = MaxId + 1;
				Records.Add(id, item with { Id = id });
			}
			await SaveAsync();
			return id;
		}

		public Task<AssemblyRecord> ReadAsync(int id) {
			throw new System.NotImplementedException();
		}

		public Task UpdateAsync(AssemblyRecord item) {
			var id = item.Id;
			lock (updatelock) {
				id = MaxId + 1;
				Records.Add(id, item with { Id = id });
			}
			return SaveAsync();
		}

		public Task DeleteAsync(int id) {
			lock (updatelock) {

			}
			throw new System.NotImplementedException();
		}

		public IEnumerable<AssemblyRecord> List() =>
			Records.Values;

		public async Task<IEnumerable<AssemblyRecord>> LoadAsync() {

			using var s = _fileSystem.Open("Data\\AssemblyRecord.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
			using var r = new StreamReader(s);
			var rtn = JsonConvert.DeserializeObject<AssemblyRecord[]>(await r.ReadToEndAsync()) ?? Array.Empty<AssemblyRecord>();
			return rtn;
		}

		public async Task SaveAsync() {
			using var s = _fileSystem.Open("Data\\AssemblyRecord.txt", FileMode.Create, FileAccess.Write, FileShare.None);
			using var r = new StreamWriter(s);
			await r.WriteAsync(JsonConvert.SerializeObject(Records.Values));
		}
	}
}