using System;

namespace PharmaRnd.Application.Products.AddBatch;

public record AddBatchCommand(
    Guid ProductId, 
    string Number, 
    DateOnly ManufacturedOn,
    IEnumerable<CreateFileDto> Files);

public record CreateFileDto(Stream Content, string FileName);