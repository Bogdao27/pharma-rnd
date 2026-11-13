using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PharmaRnd.API.Extensions;
using PharmaRnd.Application.Products.CreateProducts;
using PharmaRnd.Application.Products.Delete;
using PharmaRnd.Application.Products.UpdateMainInfo;

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

        [HttpPut("{id:guid}/main-info")]
        public async Task<ActionResult> UpdateMainInfo(
            [FromRoute] Guid id,
            [FromBody] UpdateMainInfoDto dto,
            [FromServices] UpdateMainInfoHandler handler,
            [FromServices] IValidator<UpdateMainInfoRequest> validator,
            CancellationToken cancellationToken = default)
        {
            var request = new UpdateMainInfoRequest(id, dto);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.IsValid == false)
            {
                return validationResult.ToValidationErrorResponse();
            }

            var result = await handler.HandlerAsync(request, cancellationToken);
            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return Ok(result.Value);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(
            [FromRoute] Guid id,
            [FromServices] DeleteProductHandler handler,
            [FromServices] IValidator<DeleteProductRequest> validator,
            CancellationToken cancellationToken = default)
        {
            var request = new DeleteProductRequest(id);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.IsValid == false)
            {
                return validationResult.ToValidationErrorResponse();
            }

            var result = await handler.HandlerAsync(request, cancellationToken);
            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return Ok(result.Value);
        }
    }
}
