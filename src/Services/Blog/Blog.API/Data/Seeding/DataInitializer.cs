
using BuildingBlock.Core.Enums;

namespace Blog.API.Data.Seeding;

public class DataInitializer : IDataInitializer
{
    private readonly DataContext _context;
    public DataInitializer(DataContext context)
    {
        _context = context;
    }
    public async Task InitStatus()
    {
        if (!_context.Statuses.Any())
        {
            var statuses = new List<Status>()
            {
                new Status()
                {
                    Id = PostStatusEnum.Draft,
                    Slug = "nhap",
                    Name = "Nháp"
                },
                new Status()
                {
                    Id = PostStatusEnum.Publish,
                    Slug = "xuat-ban",
                    Name = "Xuất bản"
                },
                new Status()
                {
                    Id = PostStatusEnum.Hide,
                    Slug = "an",
                    Name = "Ẩn"
                },
            };
            _context.Statuses.AddRange(statuses);
            await _context.SaveChangesAsync();
        }
    }

    public async Task SeedAsync()
    {
        await InitStatus();
    }
}
