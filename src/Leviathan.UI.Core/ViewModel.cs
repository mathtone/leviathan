using Mathtone.MIST;
using System;
using System.ComponentModel;

namespace UI.Core {

	[Notifier(NotificationMode.Implicit)]
	public class CoreViewModel : ViewModel {
		public string Title { get; set; }
	}

	public abstract class ViewModel : INotifyPropertyChanged {

		[NotifyTarget]
		protected void RaisePropertyChanged(string propertyName) {
			var eh = PropertyChanged;
			eh?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}