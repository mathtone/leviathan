using Mathtone.Sdk.Services;
using Microsoft.Extensions.Logging;

namespace Leviathan.Services.Sdk {

	public class LeviathanServiceBase : AppServiceBase, ILeviathanService {
		public LeviathanServiceBase(ILogger<LeviathanServiceBase> log) :
			base(log) {
		}
	}
	public class LeviathanServiceBase<CFG> : ConfiguredServiceBase<CFG>, ILeviathanService
		where CFG : class, new() {

		public LeviathanServiceBase(ILogger<LeviathanServiceBase<CFG>> log, CFG config) :
			base(log, config) {
		}
	}

	public interface ILeviathanService { }
}