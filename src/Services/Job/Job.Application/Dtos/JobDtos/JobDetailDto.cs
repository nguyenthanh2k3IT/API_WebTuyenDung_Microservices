namespace Job.Application.Dtos.JobDtos;

public class JobDetailDto : JobDto
{
    public string Description { get; set; } = string.Empty;
    public string Requirement { get; set; } = string.Empty;
    public string Benefit { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string WorkingTime { get; set; } = string.Empty;
}
