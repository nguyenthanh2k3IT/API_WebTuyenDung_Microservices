namespace Identity.API.Features.ProfileFeature.Dtos;

public class ProfileDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string FileName { get; set; }
    public string OriginalFileName { get; set; }
    public string FilePath { get; set; }
}
