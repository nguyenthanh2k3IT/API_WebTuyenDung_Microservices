namespace Storage.API.Dtos;

public class FileDto
{
    public string PublicId { get; set; } = string.Empty;
    public string OriginalName { get; set; } = string.Empty;
    public string Extension { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}
