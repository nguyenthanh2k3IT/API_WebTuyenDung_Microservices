namespace Job.Application.Interfaces.Data;

public interface IDataInitializer
{
    Task SeedAsync();
}
