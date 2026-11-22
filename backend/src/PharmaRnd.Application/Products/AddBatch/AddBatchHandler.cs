using System;
using System.Security.Cryptography.X509Certificates;
using CSharpFunctionalExtensions;
using PharmaRnd.Application.FileProvider;
using PharmaRnd.Application.Providers;
using PharmaRnd.Domain.Shared;

namespace PharmaRnd.Application.Products.AddBatch;

//public class AddBatchHandler
//{
//    private readonly IFileProvider _fileProvider;

//    public AddBatchHandler(IFileProvider fileProvider)
//    {
//        _fileProvider = fileProvider;
//    }

//    public async Task<Result<Guid, Error>> HandlerAsync(
//        CancellationToken cancellationToken = default)
//        {
//            _fileProvider.UploadFile(FileData fileData, cancellationToken);
//        }
//}
