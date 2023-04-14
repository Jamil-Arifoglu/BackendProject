
using System.ComponentModel.DataAnnotations.Schema;

namespace Foxic.Entities
{
    public class Color : BaseEntity
    {
        public string Name { get; set; }
        public string ColorPath { get; set; }
        public List<ClothesColorSize> ClothesColorSize { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }
        public Color()
        {
            ClothesColorSize = new();

        }
    }
}
