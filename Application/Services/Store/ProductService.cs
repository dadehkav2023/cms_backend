using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.BusinessLogic.Message;
using Application.Interfaces.ExternalApi.FileService;
using Application.Interfaces.IRepositories;
using Application.ViewModels.Store.Product;
using Domain.Entities.Store;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Store;

public class ProductService : IProductService
{
    private readonly IRepository<Product> _productRepository;
    private readonly IFileUploaderService _fileUploaderService;

    public ProductService(IUnitOfWork unitOfWork, IFileUploaderService fileUploaderService)
    {
        _fileUploaderService = fileUploaderService;
        _productRepository = unitOfWork.GetRepository<Product>();
    }

    public async Task<BusinessLogicResult<int>> SetProductAsync(RequestSetProductViewModel model, CancellationToken ct)
    {
        var messages = new List<BusinessLogicMessage>();
        try
        {
            var product = new Product();
            if (model.Id > 0)
            {
                product = _productRepository.Find(model.Id);
            }

            product.Title = model.Title;
            product.Description = model.Description;
            product.Price = model.Price;
            product.IsActive = model.IsActive;
            product.Inventory = model.Inventory;


            var uploadedFileAddress = _fileUploaderService
                .Upload(model.Files.Select(x => x).ToList())
                .ToList();
            if (!uploadedFileAddress.Any())
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                    message: MessageId.CannotUploadFile));
                return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages);
            }

            product.ImagePath = JsonSerializer.Serialize(uploadedFileAddress);

            await _productRepository.UpdateAsync(product, true);
            messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
            return new BusinessLogicResult<int>(succeeded: true, result: 0, messages: messages);
        }
        catch (Exception ex)
        {
            messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
            return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages,
                exception: ex);
        }
    }

    public async Task<BusinessLogicResult<int>> RemoveProductAsync(int id, CancellationToken ct)
    {
        var messages = new List<BusinessLogicMessage>();
        try
        {
            var product = _productRepository.Find(id);
            await _productRepository.RemoveAsync(product, true);

            messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
            return new BusinessLogicResult<int>(succeeded: true, result: id, messages: messages);
        }
        catch (Exception ex)
        {
            messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
            return new BusinessLogicResult<int>(succeeded: false, result: id, messages: messages,
                exception: ex);
        }
    }

    public async Task<BusinessLogicResult<ResponseGetAllProductViewModel>> GetAllProductsByFilterAsync(
        RequestGetAllProductByFilterViewModel model, CancellationToken ct)
    {
        var messages = new List<BusinessLogicMessage>();
        try
        {
            var query = _productRepository.DeferdSelectAll().Select(x =>
                new ResponseGetAllProductItemViewModel
                {
                    Description = x.Description,
                    Inventory = x.Inventory,
                    FilesAsJson = x.ImagePath,
                    Price = x.Price,
                    IsActive = x.IsActive,
                    Id = x.Id,
                    Title = x.Title,
                    ProductTypeEnum = x.ProductTypeEnum
                });

            var paginatedResult = await query
                .Skip((model.Page - 1) * model.PageSize)
                .Take(model.PageSize)
                .OrderByDescending(x=>x.Id)
                .ToListAsync(ct);

            var result = new ResponseGetAllProductViewModel
            {
                Count = paginatedResult.Count,
                Items = paginatedResult,
                TotalCount = query.Count(),
                CurrentPage = model.Page
            };
            messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
            return new BusinessLogicResult<ResponseGetAllProductViewModel>(succeeded: true, result: result,
                messages: messages);
        }
        catch (Exception ex)
        {
            messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
            return new BusinessLogicResult<ResponseGetAllProductViewModel>(succeeded: false, result: null,
                messages: messages,
                exception: ex);
        }
    }
    
    public async Task<BusinessLogicResult<ResponseGetAllProductViewModel>> GetAllProductsAsync(
        RequestGetAllProductByFilterViewModel model, CancellationToken ct)
    {
        var messages = new List<BusinessLogicMessage>();
        try
        {
            var query = _productRepository
                .DeferredWhere(x=>x.IsActive)
                .Where(x=>x.Inventory>0)
                .Select(x =>
                new ResponseGetAllProductItemViewModel
                {
                    Description = x.Description,
                    Inventory = x.Inventory,
                    FilesAsJson = x.ImagePath,
                    Price = x.Price,
                    Id = x.Id,
                    Title = x.Title,
                    ProductTypeEnum = x.ProductTypeEnum
                });

            var paginatedResult = await query
                .Skip((model.Page - 1) * model.PageSize)
                .Take(model.PageSize)
                .OrderByDescending(x=>x.Id)
                .ToListAsync(ct);

            var result = new ResponseGetAllProductViewModel
            {
                Count = paginatedResult.Count,
                Items = paginatedResult,
                TotalCount = query.Count(),
                CurrentPage = model.Page
            };
            messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
            return new BusinessLogicResult<ResponseGetAllProductViewModel>(succeeded: true, result: result,
                messages: messages);
        }
        catch (Exception ex)
        {
            messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
            return new BusinessLogicResult<ResponseGetAllProductViewModel>(succeeded: false, result: null,
                messages: messages,
                exception: ex);
        }
    }
}