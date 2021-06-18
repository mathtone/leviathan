namespace Leviathan.RNG {
	public interface IRandom {
		int Next();
		int Next(int maxValue);
		int Next(int minValue, int maxValue);
		double NextDouble();
		byte[] GetBytes(int values);
		void NextBytes(byte[] values);
		uint NextUint();
		string GetString(int length);

		T Pick<T>(params T[] values);
	}
}
