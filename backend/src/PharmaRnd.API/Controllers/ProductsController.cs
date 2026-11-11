using Microsoft.AspNetCore.Mvc;
using PharmaRnd.API.Extensions;
using PharmaRnd.Application.Products.CreateProducts;

namespace PharmaRnd.API.Controllers
{
    public class ProductsController : ApplicationController
    {
        [HttpPost]
        public async Task<ActionResult> Create(
            [FromServices] CreateProductHandler handler,
            [FromBody] CreateProductRequest request,
            CancellationToken cancellationToken = default)
        {
            var result = await handler.HandlerAsync(request, cancellationToken);

            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return CreatedAtAction("", result.Value);
        }
    }
}
