using Foxic.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Foxic.Utilities.Extensions
{
	public class ClothesComparer : IEqualityComparer<Clothes>
	{
		public bool Equals(Clothes? x, Clothes? y)
		{
			if (Equals(x?.Id, y?.Id)) return true;
			return false;
		}

		public int GetHashCode([DisallowNull] Clothes obj)
		{
			throw new NotImplementedException();
		}
	}
}
