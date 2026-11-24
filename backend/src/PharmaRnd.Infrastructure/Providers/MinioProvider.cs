using CSharpFunctionalExtensions;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;
using PharmaRnd.Application.FileProvider;
using PharmaRnd.Application.Providers;
using PharmaRnd.Domain.Shared;

namespace PharmaRnd.Infrastructure.Providers;

public class MinioProvider : IFilesProvider
{
    private const int PRESIGNED_LENGTH = 300;
    private const int MAX_DEGREE_OF_PARALLELISM = 10;
    
    private readonly IMinioClient _minioClient;

    public MinioProvider(IMinioClient minioClient)
    {
        _minioClient = minioClient;
    }

    public async Task<UnitResult<Error>> UploadFile(
        FileData fileData,
        CancellationToken cancellationToken = default)
    {
        var semaphoreSlim = new SemaphoreSlim(MAX_DEGREE_OF_PARALLELISM);
        
        try
        {
            var bucketExistArgs = new BucketExistsArgs().WithBucket(fileData.BucketName);
            
            var bucketExist = await _minioClient.BucketExistsAsync(bucketExistArgs, cancellationToken);
            if (bucketExist == false)
            {
                var makeBucketArgs = new MakeBucketArgs().WithBucket(fileData.BucketName);
                
                await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
            }

            List<Task> tasks = [];
            foreach (var file in fileData.Files)
            {
                await semaphoreSlim.WaitAsync(cancellationToken);

                var putObjectArgs = new PutObjectArgs()
                    .WithBucket(fileData.BucketName)
                    .WithStreamData(file.Stream)
                    .WithObjectSize(file.Stream.Length)
                    .WithObject(file.ObjectName);

                var task = _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);

                semaphoreSlim.Release();

                tasks.Add(task);    
            }

            await Task.WhenAll(tasks);
        }
        catch (Exception ex) when (ex is MinioException or HttpRequestException)
        {
            return Error.Failure("file.upload", "Fail to upload file in minio");
        }
        finally
        {
            semaphoreSlim.Release();
        }

        return Result.Success<Error>();  
    }

    public async Task<UnitResult<Error>> DeleteFile(
        string bucketName,
        string objectName,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(bucketName) || string.IsNullOrWhiteSpace(objectName))
        {
            return Errors.General.ValueIsInvalid();
        }
        
        try
        {
            var removeObjectArgs = new RemoveObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName);

            await _minioClient.RemoveObjectAsync(removeObjectArgs, cancellationToken);

            return UnitResult.Success<Error>();
        }
        catch (Exception ex) when (ex is MinioException or HttpRequestException)
        {
            return Error.Failure("file.delete", "Fail to delete file in minio");
        }
    }

    public async Task<Result<string, Error>> GetPresignedUrl(
        string bucketName,
        string objectName)
    {
        if (string.IsNullOrWhiteSpace(bucketName) || string.IsNullOrWhiteSpace(objectName))
        {
            return Errors.General.ValueIsInvalid();
        }

        try
        {
            var presignedGetObjectArgs = new PresignedGetObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName)
                .WithExpiry(PRESIGNED_LENGTH);

            var result = await _minioClient.PresignedGetObjectAsync(presignedGetObjectArgs);

            return result;
        }
        catch (Exception ex) when (ex is MinioException or HttpRequestException)
        {
            return Error.Failure("file.presign", "Fail to presign url file in minio");
        }
    }
}
