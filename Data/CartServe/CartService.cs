using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Blazored.Toast.Services;
using WatchStore.Data.ProductServe;

namespace WatchStore.Data.CartServe
{
    public class CartService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly IToastService _toastService;
        private readonly ProductService _productService;

        public event Action OnChange;
        public int cartItemCount { get; private set; } = 0;

        public CartService(ILocalStorageService localStorage, IToastService toastService, ProductService productService)
        {
            _localStorage = localStorage;
            _toastService = toastService;
            _productService = productService;
            _ = GetProductCount();
        }

        public async Task<List<CartItems>> FetchFromLocalStorage()
        {
            return await _localStorage.GetItemAsync<List<CartItems>>("userCart");
        }

        public async Task SaveOnLocalStorage(List<CartItems> saveProduct)
        {
            await _localStorage.SetItemAsync("userCart", saveProduct);
        }

        private async Task GetProductCount()
        {
            var cartProds = await FetchFromLocalStorage();
            if (cartProds == null)
            {
                cartProds = new List<CartItems>();
            }
            this.cartItemCount = cartProds != null ? cartProds.Count : 0;
            OnChange.Invoke();
        }

        public async Task AddProductToCart(Product product)
        {
            var cartProds = await FetchFromLocalStorage();
            if (cartProds == null)
            {
                cartProds = new List<CartItems>();
            }

            var result = _productService.CheckProduct(product.Id);
            if (result != null)
            {
                var cart = cartProds.FirstOrDefault(p =>
                {
                    if (p.Id == product.Id)
                    {
                        p.Quantity++;
                        p.SubTotal = p.Quantity * product.Price;
                        return true;
                    }
                    return false;
                });


                if (cart != null)
                {
                    await SaveOnLocalStorage(cartProds);
                    _toastService.ShowSuccess(product.Name, "Added to Cart");
                }
                else
                {
                    cartProds.Add(ConvertProdToCartItem(product));
                    await SaveOnLocalStorage(cartProds);
                }
            }
            else
            {
                // Quantity is zero 0
                _toastService.ShowError(product.Name + " quantity is zero (0).", "Can't Add to Cart");
            }
            await GetProductCount();
        }

        private CartItems ConvertProdToCartItem(Product product)
        {
            return new CartItems
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Image = product.Image,
                Price = product.Price,
                Quantity = 1,
                SubTotal = product.Price
            };
        }

        public async Task<List<CartItems>> GetCartItems()
        {
            var cartProds = await FetchFromLocalStorage();
            if (cartProds == null)
            {
                cartProds = new List<CartItems>();
            }
            return cartProds;
        }
    }
}
