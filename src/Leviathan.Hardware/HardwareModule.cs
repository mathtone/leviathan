using System;

namespace Leviathan.Hardware {
	public class HardwareModule : IDisposable {
		private bool disposedValue;

		public int Id { get; set; }
		public string Name { get; set; }

		protected virtual void Dispose(bool disposing) {
			if (!disposedValue) {
				if (disposing) OnDispose();
				disposedValue = true;
			}
		}

		protected virtual void OnDispose() { }
		public void Dispose() {
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}