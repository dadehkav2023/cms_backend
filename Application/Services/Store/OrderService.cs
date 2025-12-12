using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.BusinessLogic.Message;
using Application.Interfaces.IRepositories;
using Application.ViewModels.Store.Order;
using Common.EnumList;
using Domain.Entities.Identity.User;
using Domain.Entities.Store;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Exception = System.Exception;

namespace Application.Services.Store;

public class OrderService(IUnitOfWork unitOfWork, UserManager<User> userManager) : IOrderService
{
    private readonly IRepository<Order> _orderRepository = unitOfWork.GetRepository<Order>();
    private readonly IRepository<Product> _productRepository = unitOfWork.GetRepository<Product>();
    private readonly UserManager<User> _userManager = userManager;

    public async Task<BusinessLogicResult<string>> CreateOrderAsync(RequestCreateNewOrderViewModel model, string userName,
        CancellationToken ct)
    {
        var messages = new List<BusinessLogicMessage>();
        try
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                throw new Exception("user is null");
            }
            var result = "";

            var productIds = model.Items.Select(x => x.ProductId).ToList();
            var products = await _productRepository.DeferredWhere(x => productIds.Contains(x.Id)).ToListAsync(ct);

            if (products.Count != model.Items.Count)
            {
                throw new Exception("product count mismatch");
            }

            var order = new Order
            {
                PostalCode = model.PostalCode,
                Address = model.Address,
                CityOrVillageId = model.CityOrVillageId,
                UserId = user.Id,
                OrderNumber = DateTime.Now.ToString("yyyyMMddHHmmssfff") + new Random().Next(1, 9999),
                OrderItems = new List<OrderItem>()
            };
            foreach (var item in model.Items)
            {
                var product = products.FirstOrDefault(x => x.Id == item.ProductId) ??
                              throw new Exception("product not found");

                if (product.ProductTypeEnum == ProductTypeEnum.PhysicalProduct)
                {
                    if (product.Inventory < item.Quantity)
                    {
                        messages.Add(new BusinessLogicMessage(type: MessageType.Error,
                            message: MessageId.ProductInventoryNotEnough));
                        return new BusinessLogicResult<string>(succeeded: false, result: string.Empty,
                            messages: messages);
                    }

                    product.Inventory -= item.Quantity;
                }

                var orderItem = new OrderItem
                {
                    Price = product.Price,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Order = order
                };
                order.OrderItems.Add(orderItem);
            }

            order.ProductPrice = order.OrderItems.Select(x=> new
            {
                totalPrice = (decimal)x.Quantity*x.Price
            }).Sum(x=>x.totalPrice);
            order.PayablePrice = order.ProductPrice;

            await _orderRepository.AddAsync(order);
            messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
            return new BusinessLogicResult<string>(succeeded: true, result: result, messages: messages);
        }
        catch (Exception ex)
        {
            messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
            return new BusinessLogicResult<string>(succeeded: false, result: string.Empty, messages: messages,
                exception: ex);
        }
    }
}