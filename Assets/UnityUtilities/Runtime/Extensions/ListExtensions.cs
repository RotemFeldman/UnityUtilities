using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityUtilities.Extensions
{
	public static class ListExtensions
	{
		static Random rng;
		
		public static bool IsNullOrEmpty<T>(this IList<T> list)
			=> list == null || !list.Any();
		
		
		public static List<T> Clone<T>(this IList<T> list)
		{
			List<T> newList = new List<T>();
			foreach (T item in list) {
				newList.Add(item);
			}

			return newList;
		}
		
		public static void Swap<T>(this IList<T> list, int indexA, int indexB)
			=> (list[indexA], list[indexB]) = (list[indexB], list[indexA]);
		
		
		
		public static IList<T> Shuffle<T>(this IList<T> list)
		{
			if (rng == null) rng = new Random();
			int count = list.Count;
			while (count > 1) {
				--count;
				int index = rng.Next(count + 1);
				(list[index], list[count]) = (list[count], list[index]);
			}
			return list;
		}
		
		public static IList<T> Filter<T>(this IList<T> source, Predicate<T> predicate)
		{
			List<T> list = new List<T>();
			foreach (T item in source) {
				if (predicate(item)) {
					list.Add(item);
				}
			}
			return list;
		}
	}
}