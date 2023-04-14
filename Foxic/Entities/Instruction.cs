

namespace Foxic.Entities
{
    public class Instruction : BaseEntity
    {
        public string Polyester { get; set; }
        public string Lining { get; set; }
        public string Drycleaning { get; set; }
        public string Chlorine { get; set; }
        public List<Clothes> Clothes { get; set; }
        public Instruction()
        {
            Clothes = new();
        }
    }
}
