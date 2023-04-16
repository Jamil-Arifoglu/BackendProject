namespace Foxic.Entities
{
    public class BasketItem : BaseEntity
    {
        public decimal UnitPrice { get; set; }
        public int SaleQuantity { get; set; }
        public int ClothesColorSizeId { get; set; }
        public ClothesColorSize ClothesColorSize { get; set; }
    }
}
