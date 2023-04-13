using Foxic.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Foxic.Utilities.Extensions
{
	public class ClothesCategoryComparer : IEqualityComparer<ClothesCatagory>
	{
		public bool Equals(ClothesCatagory? x, ClothesCatagory? y)
		{
			if (Equals(x?.Catagory.Id, y?.Catagory.Id)) return true;
			return false;
		}

		public int GetHashCode([DisallowNull] ClothesCatagory obj)
		{
			throw new NotImplementedException();
		}

	}
}
