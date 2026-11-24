using System;
using CSharpFunctionalExtensions;
using PharmaRnd.Domain.Shared;

namespace PharmaRnd.Domain;

public record FilePath
{
    private FilePath(string path)
    {
        Path = path;
    }
    
    public string Path { get; }

    public static Result<FilePath, Error> Create(Guid path, string extension)
    {
        //некая валидация

        var fullPath = path.ToString() + "." + extension;

        return new FilePath(fullPath);
    }
}
