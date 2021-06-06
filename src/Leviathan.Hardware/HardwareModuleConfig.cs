using System;

namespace Leviathan.Hardware {

	public class HardwareModuleInfo {
		public int ModuleId { get; set; }
		public int TypeId { get; set; }
		public string Name { get; set; }
	}

	public class HardwareModuleConfig {
		public int ModuleId { get; set; }
		public string Name { get; set; }
		public Type ModuleType { get; set; }
	}

	public class HardwareModuleTypeInfo {
		public int ModuleTypeId { get; set; }
		public string Name { get; set; }
		public string TypeInfo { get; set; }
	}
}