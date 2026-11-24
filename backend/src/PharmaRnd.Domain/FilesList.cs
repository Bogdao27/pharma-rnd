using System;

namespace PharmaRnd.Domain;

public class FilesList
{
    public FilesList(IEnumerable<BatchFile> files)
    {
        Files = files.ToList();
    }
    
    public IReadOnlyList<BatchFile> Files { get; }
}
