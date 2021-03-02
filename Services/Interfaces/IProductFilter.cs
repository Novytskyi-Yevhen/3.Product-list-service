using System.Collections.Generic;

namespace ProductsWithRouting.Services.Interfaces
{
    public interface IProductFilter
    {
        public int FilterId { get; set; }
        public string FilterName { get; set; }
        public IEnumerable<ProductsWithRouting.Models.Product> Filter();
    }
}
