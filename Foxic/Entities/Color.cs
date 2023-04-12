using P230_Pronia.Entities;

namespace Foxic.Entities
{
    public class Color : BaseEntity
    {
        public string Name { get; set; }
        public string ColorPath { get; set; }
        public List<ClothesColorSize> ClothesColorSize { get; set; }
        public Color()
        {
            ClothesColorSize = new();

        }
    }
}
