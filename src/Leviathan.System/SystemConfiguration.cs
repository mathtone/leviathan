namespace Leviathan.System {
	public interface ISystemConfiguration {
		string DbLogin { get; set; }
		string DbPassword { get; set; }
		string DbName { get; set; }
		string DbServerName { get; set; }
	}
	public class SystemConfiguration : ISystemConfiguration {
		public string DbLogin { get; set; }
		public string DbPassword { get; set; }
		public string DbName { get; set; }
		public string DbServerName { get; set; }

	}
}