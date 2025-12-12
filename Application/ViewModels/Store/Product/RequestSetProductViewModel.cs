using System.Collections.Generic;
using Common.EnumList;
using Microsoft.AspNetCore.Http;

namespace Application.ViewModels.Store.Product;

public class RequestSetProductViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public ProductTypeEnum ProductTypeEnum { get; set; }
    public decimal Price { get; set; }
    public float Inventory { get; set; }
    public IFormFileCollection Files { get; set; }

}