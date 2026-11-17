using System;
using CSharpFunctionalExtensions;
using PharmaRnd.Application.FileProvider;
using PharmaRnd.Domain.Shared;

namespace PharmaRnd.Application.Providers;

public interface IFileProvider
{
    Task<Result<string, Error>> UploadFile(FileData fileData, CancellationToken cancellationToken = default);
}
