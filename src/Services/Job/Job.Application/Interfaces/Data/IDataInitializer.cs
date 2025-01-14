namespace Job.Application.Interfaces.Data;

public interface IDataInitializer
{
    Task SeedAsync();
    Task InitApplicantStatus();
    Task InitCategory();
    Task InitExperience();
    Task InitGender();
    Task InitPopular();
    Task InitProvince();
    Task InitRank();
    Task InitWorkType();
}
