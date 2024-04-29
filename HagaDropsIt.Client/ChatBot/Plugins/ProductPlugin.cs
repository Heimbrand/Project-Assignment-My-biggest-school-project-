using Microsoft.SemanticKernel;
using HagaDropsIt.Client.Services;
using System.Threading.Tasks;
using HagaDropsIt.Shared.DTOs;
using System.ComponentModel;
using HagaDropsIt.Shared.Interfaces;



namespace HagaDropsIt.Client.ChatBot.Plugins
{
   
        public class ProductPlugin 
        {
            private IProductService<ProductDto> _productService;

            public ProductPlugin(IProductService<ProductDto> productService)
            {
                _productService = productService;
            }

            [KernelFunction, Description ("Get all products")]
            public Task<IEnumerable<ProductDto>> GetAllProducts()
            {
                return _productService.GetAllProducts();
            }

            [KernelFunction, Description ("Get product by ID")]
            public Task<ProductDto?> GetProductById(int id)
            {
                return _productService.GetProductById(id);
            }

            [KernelFunction, Description("Get products by name")]
            public Task<IEnumerable<ProductDto>> GetProductsByName(string name)
            {
                return _productService.GetProductsByName(name);
            }

            [KernelFunction, Description("Add a new product")]
            public Task AddProduct(ProductDto newProduct)
            {
                return _productService.AddProduct(newProduct);
            }

            [KernelFunction, Description ("Update a product")]
            public Task UpdateProduct(ProductDto updatedProduct)
            {
                return _productService.UpdateProduct(updatedProduct);
            }

            [KernelFunction, Description("Delete a product by ID")]
            public Task DeleteProduct(int id)
            {
                return _productService.DeleteProduct(id);
            }
        }
    

}
