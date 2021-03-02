using ProductsWithRouting.Models;
using ProductsWithRouting.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ProductsWithRouting.Services
{
    public class ProductFilterService : IProductFilter
    {
        private List<Product> products;

        public int FilterId { get; set; }
        public string FilterName { get; set; }

        public ProductFilterService(Data data)
        {
            products = data.Products;
        }

        public IEnumerable<Product> Filter()
        {
            IEnumerable<Product> filtered = products;

            if (FilterId != 0)
            {
                filtered = filtered.Where(p => p.Id == FilterId);
            }

            if (!string.IsNullOrEmpty(FilterName))
            {
                filtered = filtered.Where(p => p.Name == FilterName);
            }

            return filtered;
        }
    }
}
