using System;
using CSharpFunctionalExtensions;
using PharmaRnd.Application.FileProvider;
using PharmaRnd.Domain;
using PharmaRnd.Domain.Shared;

namespace PharmaRnd.Application.Providers;

public interface IFilesProvider
{
    Task<Result<IReadOnlyList<FilePath>, Error>> UploadFile(
        IEnumerable<FileData> fileData, 
        CancellationToken cancellationToken = default);
    Task<UnitResult<Error>> DeleteFile(
        string bucketName,
        string objectName,
        CancellationToken cancellationToken = default);
    Task<Result<string, Error>> GetPresignedUrl(
        string bucketName, 
        string objectName);
}
