namespace TheLeviathan.Api {
	public class ProgramConfigurationOptions {
		public const string SectionName = "ProgramConfiguration";
		public ConfigurationMethodInfo[] ServiceConfigurationMethods { get; set; }
	}

	public class ConfigurationMethodInfo {
		public string AssemblyName { get; set; }
		public string TypeName { get; set; }
		public string MethodName { get; set; }
	}
}