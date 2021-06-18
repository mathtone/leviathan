using System;

namespace Leviathan.RNG {
	public class BufferedCryptoRNG : CryptoRNG {

		int position;
		protected byte[] buffer;

		public BufferedCryptoRNG(int bufferSize = 1024) {
			buffer = new byte[bufferSize];
			rng.GetBytes(buffer);
			position = 0;
		}

		public override void NextBytes(byte[] bytes) {

			//base.NextBytes(buffer);

			if (bytes.Length > buffer.Length - position) {
				rng.GetBytes(bytes);
			}
			else {
				Buffer.BlockCopy(buffer, position, bytes, 0, bytes.Length);
				position += bytes.Length;
			}
		}

		public override uint NextUint() {

			//return base.NextUint();

			if (position + sizeof(uint) > buffer.Length) {
				rng.GetBytes(buffer);
				position = 0;
			}
			var rtn = BitConverter.ToUInt32(buffer, position);
			position += sizeof(uint);
			return rtn;
		}
	}
}
