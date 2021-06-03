using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leviathan.UI.Core {

	public abstract class ViewModelBase : INotifyPropertyChanged {
		protected void RaisePropertyChanged(string name) {
			var eh = PropertyChanged;
			eh?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
	public class CoreViewModel {
	}
}