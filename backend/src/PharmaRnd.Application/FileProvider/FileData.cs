using PharmaRnd.Domain;

namespace PharmaRnd.Application.FileProvider;
public record FileData(Stream Stream, FilePath FilePath, string BucketName);
