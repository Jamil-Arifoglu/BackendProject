using P230_Pronia.Entities;

namespace Foxic.Entities
{
    public class ClothesCollection : BaseEntity
    {
        public int CollectionId { get; set; }
        public Collection Collection { get; set; }

        public Clothes Clothes { get; set; }

    }
}
