using System.Collections.Generic;
using Common.EnumList;
using Domain.Attributes;
using Domain.Attributes.Notification;
using Domain.Attributes.Store;
using Domain.Entities.BaseEntity;
using Domain.Entities.Financial;
using Domain.Entities.Identity.User;

namespace Domain.Entities.Store;

[Auditable]
[MainEntity]
public class Order : BaseEntityWithIdentityKey
{
    public int UserId { get; set; }
    public string OrderNumber { get; set; }
    public OrderStatusEnum OrderStatus { get; set; }
    public decimal ProductPrice { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal PayablePrice { get; set; }
    public decimal ShipmentPrice { get; set; }
    
    public int CityOrVillageId { get; set; }
    public string Address { get; set; }
    public string PostalCode { get; set; }

    public virtual User User { get; set; }
    public virtual CityOrVillage CityOrVillage { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; }
    public virtual ICollection<FinancialTransaction> FinancialTransactions { get; set; }
}

[Auditable]
[MainEntity]
public class OrderItem : BaseEntityWithIdentityKey
{
    public int ProductId { get; set; }
    public int OrderId { get; set; }
    /// <summary>
    /// به ازای هر product
    /// </summary>
    public decimal Price { get; set; }
    public float Quantity { get; set; }
    
    public virtual Product Product { get; set; }
    public virtual Order Order { get; set; }
}