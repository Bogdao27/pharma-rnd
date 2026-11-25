using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PharmaRnd.Application.Database;
using PharmaRnd.Application.FileProvider;
using PharmaRnd.Application.Providers;
using PharmaRnd.Domain;
using PharmaRnd.Domain.Shared;

namespace PharmaRnd.Application.Products.AddBatch;

public class AddBatchHandler
{
    private const string BUCKET_NAME = "files";
    
    public AddBatchHandler(
        IFilesProvider fileProvider, 
        IProductsRepository productsRepository,
        IUnitOfWork unitOfWork,
        ILogger<AddBatchHandler> logger)
    {
        _fileProvider = fileProvider;
        _productsRepository = productsRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    private readonly IFilesProvider _fileProvider;
    private readonly IProductsRepository _productsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddBatchHandler> _logger;

    public async Task<Result<Guid, Error>> HandlerAsync(
        AddBatchCommand command, 
        CancellationToken cancellationToken = default)
    {
        var transaction = await _unitOfWork.BeginTransaction(cancellationToken);

        try
        {
            var productResult = await _productsRepository
                .GetById(ProductId.Create(command.ProductId), cancellationToken);
            if (productResult.IsFailure)
            {
                return productResult.Error;
            }

            var batchId = BatchId.NewBatchId();
            var batchNumber = BatchNumber.Create(command.Number).Value;
            var manufacturedOn = command.ManufacturedOn;

            List<FileData> filesData = [];
            foreach (var file in command.Files)
            {
                var extension = Path.GetExtension(file.FileName);

                var filePath = FilePath.Create(Guid.NewGuid(), extension);
                if (filePath.IsFailure)
                {
                    return filePath.Error;
                }

                var fileContent = new FileData(
                    file.Content, 
                    filePath.Value,
                    BUCKET_NAME);

                filesData.Add(fileContent);
            }

            var batchFiles = filesData
                .Select(f => f.FilePath)
                .Select(f => new BatchFile(f))
                .ToList();

            var batch = new Batch(batchId, batchNumber, manufacturedOn, batchFiles);

            productResult.Value.AddBatch(batch);

            await _unitOfWork.SaveChanges(cancellationToken);

            var uploadResult = await _fileProvider.UploadFile(
                filesData,
                cancellationToken);
            if (uploadResult.IsFailure)
            {
                return uploadResult.Error;
            }

            transaction.Commit();

            return batch.Id.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Can not add batch to product - {id} in transaction", 
                command.ProductId);
            
            transaction.Rollback();

            return Error.Failure(
                "product.batch.failure",
                "Can not add batch to product - {id}");
        }
    }
}
