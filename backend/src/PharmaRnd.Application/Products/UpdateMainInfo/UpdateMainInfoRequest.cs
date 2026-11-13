namespace PharmaRnd.Application.Products.UpdateMainInfo
{
    public record UpdateMainInfoRequest(Guid ProductId, UpdateMainInfoDto Dto);
    public record UpdateMainInfoDto(
        string ProductName,
        string Description,
        string DosageForm);
}
