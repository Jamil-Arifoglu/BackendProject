using P230_Pronia.Entities;

namespace Foxic.Entities
{
    public class Collection : BaseEntity
    {
        public string Name { get; set; }

        public List<ClothesCollection> ClothesCollection { get; set; }

    }
}
