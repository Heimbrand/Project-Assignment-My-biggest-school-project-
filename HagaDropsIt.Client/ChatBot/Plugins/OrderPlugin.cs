using HagaDropsIt.Client.Services;
using HagaDropsIt.Shared.DTOs;
using HagaDropsIt.Shared.Interfaces;
using Microsoft.SemanticKernel;
using MongoDB.Bson;
using System.ComponentModel;

namespace HagaDropsIt.Client.ChatBot.Plugins
{
    public class OrderPlugin 
    {
        private IOrderService<OrderDto> _orderService;

        public OrderPlugin(IOrderService<OrderDto> orderService)
        {
            _orderService = orderService;
        }

        [KernelFunction, Description("Get all orders")]
        public Task<IEnumerable<OrderDto>> GetAllOrders()
        {
            return _orderService.GetAllOrders();
        }

        [KernelFunction, Description("Get order by ID")]
        public Task<OrderDto?> GetOrderById(ObjectId id)
        {
            return _orderService.GetOrderById(id);
        }

        [KernelFunction, Description("Get order by order number")]
        public Task<OrderDto?> GetOrderByOrdernumber(string ordernumber)
        {
            return _orderService.GetOrderByOrdernumber(ordernumber);
        }

        [KernelFunction, Description("Get orders by customer GUID")]
        public Task<IEnumerable<OrderDto>> GetOrdersByCustomerGuid(Guid customerGuid)
        {
            return _orderService.GetOrdersByCustomerGuid(customerGuid);
        }

        [KernelFunction, Description("Get orders by buyer email")]
        public Task<IEnumerable<OrderDto>> GetOrdersByBuyerEmail(string buyerEmail)
        {
            return _orderService.GetOrdersByBuyerEmail(buyerEmail);
        }

        [KernelFunction, Description("Add a new order")]
        public Task AddOrder(OrderDto newOrder)
        {
            return _orderService.AddOrder(newOrder);
        }

        [KernelFunction, Description("Delete an order by ID")]
        public Task DeleteOrder(ObjectId id)
        {
            return _orderService.DeleteOrder(id);
        }
    }
}
