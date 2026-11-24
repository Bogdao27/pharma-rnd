using System;

namespace PharmaRnd.Domain;

public record BatchFile
{
    public BatchFile(FilePath pathToStorage)
    {
        PathToStorage = pathToStorage;
    }
    
    public FilePath PathToStorage { get; }
    public int FileSize { get; }
}
