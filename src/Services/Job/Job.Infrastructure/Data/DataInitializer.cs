using System.Text.Json;

namespace Job.Infrastructure.Data;

public class DataInitializer : IDataInitializer
{
    private readonly DataContext _context;
    private readonly HttpClient _httpClient;
    public DataInitializer(DataContext context, HttpClient httpClient)
    {
        _context = context;
        _httpClient = httpClient;
    }

    public async Task InitCategory()
    {
        if (!_context.Categories.Any())
        {
            var categories = new List<Category>()
            {
                new Category()
                {
                    Slug = "it",
                    Name = "Công nghệ thông tin"
                },
                new Category()
                {
                    Slug = "tai-chinh",
                    Name = "Tài chính"
                },
                new Category()
                {
                    Slug = "marketing",
                    Name = "Marketing"
                },
                new Category()
                {
                    Slug = "xay-dung",
                    Name = "Xây dựng"
                },
                new Category()
                {
                    Slug = "giao-duc",
                    Name = "Giáo dục"
                },
                new Category()
                {
                    Slug = "y-te",
                    Name = "Y tế"
                },
                new Category()
                {
                    Slug = "van-tai",
                    Name = "Vận tải"
                },
                new Category()
                {
                    Slug = "nha-hang-khach-san",
                    Name = "Nhà hàng - Khách sạn"
                },
                new Category()
                {
                    Slug = "ngan-hang",
                    Name = "Ngân hàng"
                },
                new Category()
                {
                    Slug = "bat-dong-san",
                    Name = "Bất động sản"
                }
            };

            _context.Categories.AddRange(categories);
            await _context.SaveChangesAsync();
        }
    }

    public async Task InitExperience()
    {
        if (!_context.Experiences.Any())
        {
            var experiences = new List<Experience>()
            {
                new Experience()
                {
                    Slug = "duoi-1-nam",
                    Name = "Dưới 1 năm"
                },
                new Experience()
                {
                    Slug = "1",
                    Name = "1 năm"
                },
                new Experience()
                {
                    Slug = "2",
                    Name = "2 năm"
                },
                new Experience()
                {
                    Slug = "3",
                    Name = "3 năm"
                },
                new Experience()
                {
                    Slug = "4",
                    Name = "4 năm"
                },
                new Experience()
                {
                    Slug = "5",
                    Name = "5 năm"
                },
                new Experience()
                {
                    Slug = "tren-5-nam",
                    Name = "Trên 5 năm"
                }
            };
            _context.Experiences.AddRange(experiences);
            await _context.SaveChangesAsync();
        }
    }

    public async Task InitGender()
    {
        if (!_context.Genders.Any())
        {
            var genders = new List<Gender>()
            {
                new Gender()
                {
                    Slug = "nam",
                    Name = "Nam"
                },
                new Gender()
                {
                    Slug = "nu",
                    Name = "Nữ"
                }
            };
            _context.Genders.AddRange(genders);
            await _context.SaveChangesAsync();
        }
    }

    public async Task InitPopular()
    {
        if (!_context.Populars.Any())
        {
            var populars = new List<Popular>()
            {
                new Popular()
                {
                    Slug = "binh-thuong",
                    Name = "Bình thường",
                    Background = "",
                    TargetApplicants = 0
                },
                new Popular()
                {
                    Slug = "hot",
                    Name = "Hot",
                    Background = "",
                    TargetApplicants = 100
                },
                new Popular()
                {
                    Slug = "super-hot",
                    Name = "Super Hot",
                    Background = "",
                    TargetApplicants = 200
                },
            };
            _context.Populars.AddRange(populars);
            await _context.SaveChangesAsync();
        }
    }

    public class ProvinceOpenAPI
    {
        public int code { get; set; }
        public string name { get; set; }
        public string division_type { get; set; }
        public string codename { get; set; }
        public int phone_code { get; set; }
    }

    public async Task InitProvince()
    {
        if (!_context.Provinces.Any())
        {
            var url = "https://provinces.open-api.vn/api/";
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string jsonResponse = await response.Content.ReadAsStringAsync();
            var datas = JsonSerializer.Deserialize<List<ProvinceOpenAPI>>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            if (datas != null && datas.Any())
            {
                List<Province> provinces = new List<Province>();
                foreach (var item in datas)
                {
                    var province = new Province();
                    province.Id = item.code.ToString();
                    province.Name = item.name;
                    province.Area = item.phone_code.ToString();
                    province.AreaName = item.division_type;
                    province.Code = item.codename;
                    province.CreatedUser = Guid.NewGuid();
                    province.ModifiedUser = Guid.NewGuid();

                    provinces.Add(province);
                }
                _context.Provinces.AddRange(provinces);
                await _context.SaveChangesAsync();
            }
        }
    }

    public async Task InitRank()
    {
        if (!_context.Ranks.Any())
        {
            var ranks = new List<Rank>()
            {
                new Rank()
                {
                    Slug = "nhan-vien",
                    Name = "Nhân viên"
                },
                new Rank()
                {
                    Slug = "truong-nhom",
                    Name = "Trưởng nhóm"
                },
                new Rank()
                {
                    Slug = "truong-pho-phong",
                    Name = "Trưởng/Phó phòng"
                },
                new Rank()
                {
                    Slug = "quan-ly-giam-sat",
                    Name = "Quản lý / Giám sát"
                },
                new Rank()
                {
                    Slug = "truong-chi-nhanh",
                    Name = "Trưởng chi nhánh"
                },
                new Rank()
                {
                    Slug = "pho-giam-doc",
                    Name = "Phó giám đốc"
                },
                new Rank()
                {
                    Slug = "giam-doc",
                    Name = "Giám đốc"
                },
                new Rank()
                {
                    Slug = "thuc-tap-sinh",
                    Name = "Thực tập sinh"
                }
            };

            _context.Ranks.AddRange(ranks);
            await _context.SaveChangesAsync();
        }
    }

    public async Task InitWorkType()
    {
        if (!_context.WorkTypes.Any())
        {
            var workTypes = new List<WorkType>()
            {
                new WorkType()
                {
                    Slug = "thuc-tap",
                    Name = "Thực tập"
                },
                new WorkType()
                {
                    Slug = "ban-thoi-gian",
                    Name = "Bán thời gian"
                },
                new WorkType()
                {
                    Slug = "toan-thoi-gian",
                    Name = "Toàn thời gian"
                }
            };
            _context.WorkTypes.AddRange(workTypes);
            await _context.SaveChangesAsync();
        }
    }

    public async Task InitApplicantStatus()
    {
        if (!_context.ApplicantStatuses.Any())
        {
            var status = new List<ApplicantStatus>()
            {
                new ApplicantStatus()
                {
                    Id = ApplicantStatusEnum.Applied,
                    Name = "Đã ứng tuyển",
                    Sort = 0
                },
                new ApplicantStatus()
                {
                    Id = ApplicantStatusEnum.Seen,
                    Name = "Nhà tuyển dụng đã xem",
                    Sort = 1
                },
                new ApplicantStatus()
                {
                    Id = ApplicantStatusEnum.Unsuitable,
                    Name = "Không phù hợp",
                    Sort = 2
                },
                new ApplicantStatus()
                {
                    Id = ApplicantStatusEnum.Suitable,
                    Name = "Ứng viên phù hợp",
                    Sort = 2
                }
            };
            _context.ApplicantStatuses.AddRange(status);
            await _context.SaveChangesAsync();
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await InitApplicantStatus();
            await InitCategory();
            await InitExperience();
            await InitGender();
            await InitPopular();
            await InitProvince();
            await InitRank();
            await InitWorkType();
        }
        catch(Exception ex)
        {
            Console.WriteLine($"[Seeding] - Error - {ex.Message}");
        }
    }
}
