using Leviathan.Services.SDK;

namespace Leviathan.Alpha.FileDataSystem {
	public interface IFileDataSystemService : ILeviathanService {

	}

	[SingletonService(typeof(IFileDataSystemService))]
	public class FileDataSystemService : LeviathanService, IFileDataSystemService {

	}
}