using System.Collections.Generic;

namespace Application.ViewModels.Store.Order;

public class RequestCreateNewOrderViewModel
{
    public int CityOrVillageId { get; set; }
    public string Address { get; set; }
    public string PostalCode { get; set; }
    public List<RequestCreateNewOrderItemViewModel> Items { get; set; }
}

public class RequestCreateNewOrderItemViewModel
{
    public int ProductId { get; set; }
    public float Quantity { get; set; }
}