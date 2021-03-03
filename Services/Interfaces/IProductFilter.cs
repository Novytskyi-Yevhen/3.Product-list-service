using ProductsWithRouting.Models;
using System.Collections.Generic;

namespace ProductsWithRouting.Services.Interfaces
{
    public interface IProductFilter
    {
        public IEnumerable<Product> FilterBy(Product product);
    }
}
