using Job.Domain.Enums;

namespace Job.Domain.Entities;

[Table("tb_jobs")]
public class Job : BaseEntity<Guid>
{
    public Job() : base() { }   
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public SalaryType SalaryType { get; set; }
    public decimal SalaryMin { get; set; }
    public decimal SalaryMax { get; set; }
    public int Quantity { get; set; } = 10;
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; } = DateTime.Now.AddDays(30);
    public bool Available { get; set; } = true;

    #region Detail job 
    public string Description { get; set; } = string.Empty;
    public string Requirement { get; set; } = string.Empty;
    public string Benefit { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string WorkingTime { get; set; } = string.Empty;
    #endregion

    #region Foreign key
    public Guid? CompanyId { get; set; }
    public Guid? CategoryId { get; set; }
    public Guid? RankId { get; set; }
    public Guid? GenderId { get; set; }
    public Guid? ExperienceId { get; set; }
    public Guid? WorkTypeId { get; set; }
    public Guid? PopularId { get; set; }
    #endregion

    #region Foreign entity
    [JsonIgnore] public RepCompany? Company { get; set; }
    [JsonIgnore] public Category? Category { get; set; }
    [JsonIgnore] public Rank? Rank { get; set; }
    [JsonIgnore] public Gender? Gender { get; set; }
    [JsonIgnore] public Experience? Experience { get; set; }
    [JsonIgnore] public WorkType? WorkType { get; set; }
    [JsonIgnore] public Popular? Popular { get; set; }
    [JsonIgnore] public ICollection<Applicant>? Applicants { set; get; }
    [JsonIgnore] public ICollection<Province>? WorkLocations { set; get; }
    #endregion
}