using CSharpFunctionalExtensions;
using PharmaRnd.Application.FileProvider;
using PharmaRnd.Application.Providers;
using PharmaRnd.Domain;
using PharmaRnd.Domain.Shared;

namespace PharmaRnd.Application.Products.AddBatch;

public class AddBatchHandler
{
    private const string BUCKET_NAME = "files";
    
    public AddBatchHandler(IFilesProvider fileProvider, IProductsRepository productsRepository)
    {
        _fileProvider = fileProvider;
        _productsRepository = productsRepository;
    }
    
    private readonly IFilesProvider _fileProvider;
    private readonly IProductsRepository _productsRepository;

    public async Task<Result<Guid, Error>> HandlerAsync(
        AddBatchCommand command, 
        CancellationToken cancellationToken = default)
    {
        var productResult = await _productsRepository.GetById(
            ProductId.Create(command.ProductId), cancellationToken);
        if (productResult.IsFailure)
        {
            return productResult.Error;
        }

        var batchId = BatchId.NewBatchId();
        var batchNumber = BatchNumber.Create(command.Number).Value;
        var manufacturedOn = command.ManufacturedOn;

        List<FileContent> fileContents = [];
        foreach (var file in command.Files)
        {
            var extension = Path.GetExtension(file.FileName);

            var filePath = FilePath.Create(Guid.NewGuid(), extension);
            if (filePath.IsFailure)
            {
                return filePath.Error;
            }

            var fileContent = new FileContent(file.Content, filePath.Value.Path);

            fileContents.Add(fileContent);
        }

        var fileData = new FileData(fileContents, BUCKET_NAME);

        var uploadResult = await _fileProvider.UploadFile(fileData, cancellationToken);
        if (uploadResult.IsFailure)
        {
            return uploadResult.Error;
        }

        var filePaths = command.Files.Select(f => FilePath.Create(Guid.NewGuid(), f.FileName));

        var batchFiles = filePaths.Select(f => new BatchFile(f.Value));

        var batch = new Batch(
            batchId,
            batchNumber,
            manufacturedOn,
            new FilesList(batchFiles));

        productResult.Value.AddBatch(batch);

        await _productsRepository.Save(productResult.Value, cancellationToken);

        return batch.Id.Value;
    }
}
