using System;

namespace Leviathan.RNG {
	public class SimpleRNG : Random, IRandom {
		const string ALPHA_CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
		public SimpleRNG() { }
		public SimpleRNG(int seed) : base(seed) { }

		public byte[] GetBytes(int count) {
			var rtn = new byte[count];
			NextBytes(rtn);
			return rtn;
		}

		public string GetString(int length) {

			var chars = new char[length];

			for (int i = 0; i < chars.Length; i++) {
				chars[i] = ALPHA_CHARS[Next(ALPHA_CHARS.Length)];
			}

			return new string(chars);
		}

		public virtual uint NextUint() {
			var buffer = new byte[sizeof(uint)];
			NextBytes(buffer);
			return BitConverter.ToUInt32(buffer, 0);
		}

		public T Pick<T>(params T[] values) => values[Next(values.Length)];
	}
}
