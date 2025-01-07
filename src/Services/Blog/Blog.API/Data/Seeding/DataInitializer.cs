
using BuildingBlock.Core.Constants;
using BuildingBlock.Core.Enums;
using BuildingBlock.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Data.Seeding;

public class DataInitializer : IDataInitializer
{
    private readonly DataContext _context;
    public DataInitializer(DataContext context)
    {
        _context = context;
    }

    public async Task InitCategory()
    {
        if (!_context.Categories.Any())
        {
            var categories = new List<Category>()
        {
            new Category()
            {
                Slug = "cong-nghe",
                Name = "Công nghệ"
            },
            new Category()
            {
                Slug = "suc-khoe",
                Name = "Sức khỏe"
            },
            new Category()
            {
                Slug = "giao-duc",
                Name = "Giáo dục"
            },
            new Category()
            {
                Slug = "phong-cach-song",
                Name = "Phong cách sống"
            },
            new Category()
            {
                Slug = "kinh-doanh",
                Name = "Kinh doanh"
            },
        };

            _context.Categories.AddRange(categories);
            await _context.SaveChangesAsync();
        }
    }

    public async Task InitPost()
    {
        if (!_context.Posts.Any())
        {
            string template = "" +
                "<h1>Chào mừng bạn đến với CKEditor!</h1>" +
                "<p>Đây là một đoạn văn bản đơn giản để thử nghiệm CKEditor.</p>" +
                "<ul>" +
                    "<li>Điều 1: CKEditor hỗ trợ định dạng văn bản</li>" +
                    "<li>Điều 2: Bạn có thể thêm danh sách, bảng, hình ảnh, v.v.</li>" +
                "</ul>" +
                "<p>Hãy thử chỉnh sửa văn bản này để kiểm tra các tính năng của CKEditor.</p>";

            List<string> titles = new List<string>
            {
                "10 Bí Quyết Thành Công Trong Công Việc",
                "Hướng Dẫn Tự Học Lập Trình Từ Con Số 0",
                "Những Sai Lầm Thường Gặp Khi Đầu Tư Chứng Khoán",
                "Lợi Ích Của Việc Uống Nước Đúng Cách Mỗi Ngày",
                "Top 5 Cuốn Sách Truyền Cảm Hứng Bạn Nên Đọc",
                "Cách Tăng Cường Sức Đề Kháng Trong Mùa Đông",
                "Khám Phá Những Địa Điểm Du Lịch Đẹp Nhất Việt Nam",
                "Bí Quyết Giảm Cân Hiệu Quả Và Lâu Dài",
                "Tại Sao Bạn Nên Thử Thách Bản Thân Với Những Điều Mới",
                "Làm Thế Nào Để Duy Trì Cân Bằng Công Việc Và Cuộc Sống"
            };
            Random random = new Random();
            var posts = new List<Post>();
            for (var i = 1; i <= 50; i++)
            {
                var code = Generator.GenerateCode();
                int randomIndex = random.Next(titles.Count);
                string randomTitle = titles[randomIndex];
                var post = new Post()
                {
                    Slug = code,
                    Title = $"{code} {randomTitle}",
                    Image = AvatarConstant.Default,
                    Category = await _context.Categories.OrderBy(s => Guid.NewGuid()).FirstOrDefaultAsync(),
                    Status = await _context.Statuses.OrderBy(s => Guid.NewGuid()).FirstOrDefaultAsync(),
                    TagNames = await _context.TagNames.OrderBy(s => Guid.NewGuid()).Take(2).ToListAsync(),
                    Content = template
                };
                posts.Add(post);
            }

            _context.Posts.AddRange(posts);
            await _context.SaveChangesAsync();
        }
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

    public async Task InitTagName()
    {
        if (!_context.TagNames.Any())
        {
            var tagNames = new List<TagName>()
            {
                new TagName()
                {
                    Slug = "cong-nghe",
                    Name = "Công nghệ"
                },
                new TagName()
                {
                    Slug = "doi-song",
                    Name = "Đời sống"
                },
                new TagName()
                {
                    Slug = "tuyen-dung",
                    Name = "Tuyển dụng"
                },
                new TagName()
                {
                    Slug = "chia-se",
                    Name = "Chia sẻ"
                },
            };
            _context.TagNames.AddRange(tagNames);
            await _context.SaveChangesAsync();
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await InitCategory();
            await InitStatus();
            await InitTagName();
            await InitPost();
        }
        catch (Exception ex) { }
    }
}
