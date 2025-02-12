using Job.Domain.Enums;

namespace Job.Application.Dtos;

public class JobDto
{
    public Guid Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public SalaryType SalaryType { get; set; }
    public decimal SalaryMin { get; set; }
    public decimal SalaryMax { get; set; }
    public int Quantity { get; set; } = 10;
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; } = DateTime.Now.AddDays(30);
    public bool Available { get; set; } = true;
    public RepCompanyDto? Company { get; set; }
    public CategoryDto? Category { get; set; }
    public RankDto? Rank { get; set; }
    public GenderDto? Gender { get; set; }
    public WorkTypeDto? WorkType { get; set; }
    public PopularDto? Popular { get; set; }
}
