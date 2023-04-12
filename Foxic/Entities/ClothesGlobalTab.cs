using P230_Pronia.Entities;

namespace Foxic.Entities
{
    public class ClothesGlobalTab : BaseEntity
    {
        public string GlobalTab { get; set; }

        public List<Clothes> Clothes { get; set; }
    }
}
