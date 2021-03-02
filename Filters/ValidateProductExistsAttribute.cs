using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProductsWithRouting.Models;
using ProductsWithRouting.Services;
using System.Collections.Generic;
using System.Linq;

namespace ProductsWithRouting.Filters
{
    public class ValidateProductExistsAttribute : TypeFilterAttribute
    {
        public ValidateProductExistsAttribute()
            : base(typeof(ValidateProductExistsFilter))
        {
        }

        private class ValidateProductExistsFilter : ActionFilterAttribute
        {
            private readonly List<Product> products;

            public ValidateProductExistsFilter(Data data)
            {
                products = data.Products;
            }

            public override void OnActionExecuting(ActionExecutingContext context)
            {
                if (context.ActionArguments.ContainsKey("id"))
                {
                    var id = context.ActionArguments["id"] as int?;
                    if (id.HasValue)
                    {
                        if (products.All(a => a.Id != id.Value))
                        {
                            context.Result = new RedirectToActionResult("Error404", "products", null);
                            return;
                        }
                    }
                }
            }
        }
    }
}
