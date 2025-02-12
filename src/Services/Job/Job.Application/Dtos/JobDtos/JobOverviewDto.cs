namespace Job.Application.Dtos.JobDtos;

public class JobOverviewDto
{
    public Guid Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public RepCompanyDto? Company { get; set; }
}
