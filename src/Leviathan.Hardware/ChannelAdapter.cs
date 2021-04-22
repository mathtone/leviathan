using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leviathan.Hardware {	

	//public class InputChannelAdapter<T, C> : InputChannelController<T, C>, IInputChannel<T> {

	//	protected Func<C, T> getter;
	//	public override T Value => getter(Channel);
	//	public InputChannelAdapter(C channel, Func<C, T> getter) : base(channel) {
	//		this.getter = getter;
	//	}
	//}

	//public class OutputChannelAdapter<T, C> : OutputChannelController<T, C>, IOutputChannel<T> {

	//	protected Action<C, T> setter;
	//	public override T Value { set => setter(Channel, value); }

	//	public OutputChannelAdapter(C device, Action<C, T> setter) : base(device) {
	//		this.setter = setter;
	//	}
	//}

	//public class InputOutputChannelAdapter<T, C> : InputOutputChannelController<T, C>, InputOutputChannel<T> {
	//	protected Func<C, T> getter;
	//	protected Action<C, T> setter;

	//	public override T Value {
	//		get => getter(Channel);
	//		set => setter(Channel, value);
	//	}

	//	public InputOutputChannelAdapter(C channel, Func<C, T> getter, Action<C, T> setter) : base(channel) {
	//		this.getter = getter;
	//		this.setter = setter;
	//	}
	//}
}