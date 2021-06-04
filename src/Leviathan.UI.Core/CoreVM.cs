using Mathtone.MIST;

namespace UI.Core {

	[Notifier(NotificationMode.Implicit)]
	public class CoreVM : ViewModel {
		public string Title { get; protected set; }
	}


	public class ModuleVM : ViewModel {
		public string Name { get; set; }
	}
}