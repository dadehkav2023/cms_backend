using Common.EnumList;

namespace Application.ViewModels.Store.Order
{
    public class ResponseGetCurrentUserOrdersViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string OrderNumber { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }
        public string OrderStatusTitle => OrderStatus.GetEnumDescription();
        public decimal ProductPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal PayablePrice { get; set; }
        public decimal ShipmentPrice { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
    }
}
