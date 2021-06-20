namespace Leviathan.Alpha.Configuration {

	public interface IConfigProvider<out CFG> {
		CFG Config { get; }
	}

	public interface IConfigManager<CFG> : IConfigProvider<CFG> {
		void Save(CFG config);
	}

	public abstract class ConfigServiceBase<CFG> : IConfigManager<CFG> {
		public abstract CFG Config { get; protected set; }

		public void Save(CFG config) {
			throw new System.NotImplementedException();
		}
	}
}