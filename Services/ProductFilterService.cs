using ProductsWithRouting.Models;
using ProductsWithRouting.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ProductsWithRouting.Services
{
    public class ProductFilterService : IProductFilter
    {
        private List<Product> products;

        public ProductFilterService(Data data)
        {
            products = data.Products;
        }

        public IEnumerable<Product> FilterBy(Product product)
        {
            IEnumerable<Product> filtered = products;
            if (product.Id != 0)
            {
                filtered = filtered.Where(p => p.Id == product.Id);
            }
            if (!string.IsNullOrEmpty(product.Name))
            {
                filtered = filtered.Where(p => p.Name.Contains(product.Name));
            }
            if(!string.IsNullOrEmpty(product.Description))
            {
                filtered = filtered.Where(p => p.Description.Contains(product.Description));
            }
            return filtered;
        }
    }
}
