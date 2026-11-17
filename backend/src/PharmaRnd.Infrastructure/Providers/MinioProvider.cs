using CSharpFunctionalExtensions;
using Minio;
using Minio.DataModel.Args;
using PharmaRnd.Application.FileProvider;
using PharmaRnd.Application.Providers;
using PharmaRnd.Domain.Shared;

namespace PharmaRnd.Infrastructure.Providers;

public class MinioProvider : IFileProvider
{
    private readonly MinioClient _minioClient;

    public MinioProvider(MinioClient minioClient)
    {
        _minioClient = minioClient;
    }

    public async Task<Result<string, Error>> UploadFile(
        FileData fileData,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var bucketExistArgs = new BucketExistsArgs().WithBucket("photos");
            
            var bucketExist = await _minioClient.BucketExistsAsync(bucketExistArgs, cancellationToken);
            if (bucketExist == false)
            {
                var makeBucketArgs = new MakeBucketArgs().WithBucket("photos");
                
                await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
            }
            
            var path = Guid.NewGuid();
            
            var putObjectArgs = new PutObjectArgs()
                 .WithBucket("photos")
                 .WithStreamData(fileData.Stream)
                 .WithObjectSize(fileData.Stream.Length)
                 .WithObject(path.ToString());

            var result = await _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);

            return result.ObjectName;
        }
        catch
        {
            return Error.Failure("file.upload", "Fail to upload file in minio");
        }  
    }
}
