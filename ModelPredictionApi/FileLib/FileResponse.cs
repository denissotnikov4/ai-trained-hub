namespace FileLib;

public record FileResponse
{
    /// <summary>
    /// Full file name
    /// </summary>
    public string FullFileName { get; init; }

    /// <summary>
    /// File content
    /// </summary>
    public byte[] FileContent { get; init; }
};