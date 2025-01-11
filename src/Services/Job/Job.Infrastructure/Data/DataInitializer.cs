namespace Job.Infrastructure.Data;

internal class DataInitializer : IDataInitializer
{
    public Task SeedAsync()
    {
        return Task.CompletedTask;
    }
}
