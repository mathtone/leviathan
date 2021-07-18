using System.Collections.Generic;

namespace Leviathan.Alpha.SystemConfiguration {
	public class ProfileApplication {
		public string ProfileName { get; init; }
		public IEnumerable<ApplicationField> ApplicationFields { get; init; }
	}

	public class ApplicationField {
		public string Name { get; set; }
		public string Description { get; set; }
		public object Value { get; set; }
	}
}