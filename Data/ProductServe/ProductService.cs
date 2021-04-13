using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WatchStore.Data.CartServe;
using Microsoft.EntityFrameworkCore;

namespace WatchStore.Data.ProductServe
{
    public class ProductService
    {
        public ProductService()
        {
        }

        private WatchStoreDbContext dbContext;

        public event Action OnChange;

        // Contructor
        public ProductService(WatchStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Product>> GetProductAsync()
        {
            return await dbContext.Product.ToListAsync();
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            try
            {
                dbContext.Product.Add(product);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return product;
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            try
            {
                var productExist = dbContext.Product.FirstOrDefault(p => p.Id == product.Id);
                if (productExist != null)
                {
                    dbContext.Update(product);
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return product;
        }

        public Product GetSingleProduct(int prodID)
        {
            //var product = new Product();
            try
            {
                var productExist = dbContext.Product.FirstOrDefault(p => p.Id == prodID);
                if (productExist != null)
                {
                    return productExist;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return null;
        }

        public Product CheckProduct(int prodID)
        {
            var product = new Product();
            try
            {
                var productExist = dbContext.Product.FirstOrDefault(p => p.Id == prodID);
                if (productExist != null && productExist.Quantity > 0)
                {
                    productExist.Quantity--;
                    //product = productExist;
                    _ = UpdateProductAsync(productExist);
                    OnChange.Invoke();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return product;
        }

        public async Task DeleteProductAsync(Product product)
        {
            try
            {
                dbContext.Product.Remove(product);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
