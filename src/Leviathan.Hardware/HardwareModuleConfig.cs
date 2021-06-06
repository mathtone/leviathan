using System;

namespace Leviathan.Hardware {

	public class HardwareModuleInfo {
		public int Id { get; set; }
		public int TypeId { get; set; }
		public string Name { get; set; }

		public HardwareModuleInfo() { }
		public HardwareModuleInfo(int typeId, string name, int id=default) {
			this.Id = id;
			this.TypeId = typeId;
			this.Name = name;
		}
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

		public HardwareModuleTypeInfo() { }
		public HardwareModuleTypeInfo(string typeInfo, string name, int id = default) {
			this.ModuleTypeId = id;
			this.TypeInfo = typeInfo;
			this.Name = name;
		}
	}
}