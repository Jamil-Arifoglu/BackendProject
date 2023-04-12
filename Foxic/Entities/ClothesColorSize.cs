using P230_Pronia.Entities;

namespace Foxic.Entities
{
    public class ClothesColorSize : BaseEntity
    {

        public int ClothesId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public int Quantity { get; set; }
        public Clothes Clothes { get; set; }
        public Color Color { get; set; } = null!;
        public Size Size { get; set; } = null!;
    }
}
