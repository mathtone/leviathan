using Mathtone.MIST;
using System;
using System.ComponentModel;

namespace UI.Core {

	public abstract class ViewModel : INotifyPropertyChanged {

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyTarget]
		protected void RaisePropertyChanged(string propertyName) {
			var eh = PropertyChanged;
			eh?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}