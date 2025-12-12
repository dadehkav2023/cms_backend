using System.Collections.Generic;
using System.Text.Json.Serialization;
using Application.ViewModels.Public;
using Common.EnumList;
using RestSharp.Serialization.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Application.ViewModels.Store.Product;

public class ResponseGetAllProductViewModel : ResponseGetListViewModel
{
    public List<ResponseGetAllProductItemViewModel> Items { get; set; }
}

public class ResponseGetAllProductItemViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public ProductTypeEnum ProductTypeEnum { get; set; }
    public string ProductTypeTitle => ProductTypeEnum.GetEnumDescription();
    public decimal Price { get; set; }
    public float Inventory { get; set; }
    [JsonIgnore] public string FilesAsJson { get; set; }

    public List<string> Files => !string.IsNullOrEmpty(FilesAsJson)
        ? JsonSerializer.Deserialize<List<string>>(FilesAsJson)
        : null;
}