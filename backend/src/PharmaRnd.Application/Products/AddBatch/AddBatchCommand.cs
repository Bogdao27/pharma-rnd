using System;

namespace PharmaRnd.Application.Products.AddBatch;

public record AddBatchCommand(
    Guid ProductId, 
    string Number, 
    DateOnly ManufacturedOn,
    IEnumerable<FileDto> Files);

public record FileDto(Stream Content, string FileName, string ContentType);