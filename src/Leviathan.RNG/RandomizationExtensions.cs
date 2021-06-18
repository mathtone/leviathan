using System.Collections.Generic;
using System.Linq;

namespace Leviathan.RNG {
	public static class RandomizationExtensions {

		public static IEnumerable<T> Randomize<T>(this IRandom randomSource, IList<T> items) {
			var indices = Enumerable.Range(0, items.Count).ToArray();
			randomSource.UnSort(indices);
			foreach (var i in indices) {
				yield return items[indices[i]];
			}
		}
		public static IEnumerable<T> Randomize<T>(this IRandom randomSource, IEnumerable<T> items) {
			var indices = Enumerable.Range(0, items.Count()).ToArray();
			randomSource.UnSort(indices);
			foreach (var i in indices) {
				yield return items.ElementAt(indices[i]);
			}
		}

		public static void UnSort<T>(this IRandom randomSource, IList<T> items) {
			for (var i = items.Count; i > 1; i--) {
				var a = randomSource.Next(i);
				var b = i - 1;
				var t = items[a];
				items[a] = items[b];
				items[b] = t;
			}
		}

		public static T Random<T>(this IEnumerable<T> items, IRandom randomSource) =>
			items.ElementAt(randomSource.Next(items.Count()));


		public static T Random<T>(this IList<T> items, IRandom randomSource) =>
			items[randomSource.Next(items.Count)];

	}
}
