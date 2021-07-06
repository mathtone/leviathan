using System;
using System.Threading.Tasks;
using Leviathan.Services.SDK;

namespace Leviathan.System.SDK {

	public interface ILeviathanSystem : ILeviathanService {
		public Task<SystemServiceCatalog> Catalog();
	}
}