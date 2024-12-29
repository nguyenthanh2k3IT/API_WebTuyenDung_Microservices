using System;
using System.Text.Json;

namespace Identity.API.Data.Seeding;

public class DataContextInitializer : IDataContextInitializer
{
	private readonly DataContext _context;
    private readonly HttpClient _httpClient;
    public DataContextInitializer(DataContext context, HttpClient httpClient)
	{
		_context = context;
        _httpClient = httpClient;
    }

    public class ProvinceOpenAPI
    {
        public int code { get; set; }
        public string name { get; set; }
        public string division_type { get; set; }
        public string codename { get; set; }
        public int phone_code { get; set; }
    }

    public async Task<int> InitProvince()
    {
		int rows = 0;
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
				foreach(var item in datas)
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
				rows = await _context.SaveChangesAsync();
			}
        }
		return rows;
    }

    public async Task<int> InitRole()
	{
		int rows = 0;
		if (!_context.Roles.Any())
		{
			var admin = new Role()
			{
				Id = RoleEnum.ADMIN,
				Name = "Quản trị viên",
				Description = "Quản trị viên"
			};
			var jobSeeker = new Role()
			{
				Id = RoleEnum.JOBSEEKER,
				Name = "Người tìm việc",
				Description = "Người tìm việc"
            };
            var company = new Role()
            {
                Id = RoleEnum.COMPANY,
                Name = "Doanh nghiệp",
                Description = "Doanh nghiệp"
            };
            _context.Roles.Add(admin);
			_context.Roles.Add(jobSeeker);
            _context.Roles.Add(company);
            rows = await _context.SaveChangesAsync();
		}
		return rows;
	}

    public async Task<int> InitSize()
    {
		int rows = 0;
		if (!_context.Sizes.Any())
		{
			var sizes = new List<Size>()
			{
				new Size()
				{
					Name = "Nhỏ (10-24 nhân viên)",
					Value = "10-24 nhân viên"
				},
				new Size()
				{
					Name = "Trung bình nhỏ (25-99 nhân viên)",
					Value = "25-99 nhân viên"
				},
				new Size()
				{
					Name = "Trung bình (100-499 nhân viên)",
					Value = "100-499 nhân viên"
				},
				new Size()
				{
					Name = "Trung bình lớn (500-999 nhân viên)",
					Value = "500-999 nhân viên"
				},
				new Size()
				{
					Name = "Lớn (1000+ nhân viên)",
					Value = "1000+ nhân viên"
				},
				new Size()
				{
					Name = "Siêu lớn (10000+ nhân viên)",
					Value = "10000+ nhân viên"
				},
			};
			_context.AddRange(sizes);
			rows = await _context.SaveChangesAsync();
		}
		return rows;
    }

    public async Task<int> InitStatus()
	{
		int rows = 0;
		if (!_context.Statuses.Any())
		{
			var active = new Status()
			{
				Id = UserStatusEnum.ACTIVE,
				Name = "Hoạt động",
				Description = "Hoạt động"
			};
            var unActive = new Status()
            {
                Id = UserStatusEnum.UNACTIVE,
                Name = "Chưa kích hoạt",
                Description = "Chưa kích hoạt"
            };
            var banned = new Status()
			{
				Id = UserStatusEnum.BANNED,
				Name = "Đã bị khóa",
				Description = "Đã bị khóa"
			};
			_context.Statuses.Add(active);
            _context.Statuses.Add(unActive);
            _context.Statuses.Add(banned);
			rows = await _context.SaveChangesAsync();
		}
		return rows;
	}

	public async Task<int> InitUser()
	{
		int rows = 0;
		if (!_context.Users.Any())
		{
			string address = "273 Đ. An Dương Vương, Phường 3, Quận 5, Hồ Chí Minh , Việt Nam";
			var admin = new User()
			{
				Email = "admin",
				Fullname = "admin",
				Avatar = AvatarConstant.Default,
				Password = "123456",
				Phone = "0123456789",
				RoleId = RoleEnum.ADMIN,
				StatusId = UserStatusEnum.ACTIVE
			};
			_context.Users.Add(admin);

			for(int i = 1; i <= 20; i++)
			{
				var customer = new User()
				{
					Email = $"customer{i}@gmail.com",
					Fullname = $"customer{i}",
					Avatar = AvatarConstant.Default,
					Password = "123456",
					Phone = "0123456789",
					RoleId = RoleEnum.JOBSEEKER,
					StatusId = UserStatusEnum.ACTIVE
				};
				_context.Users.Add(customer);

				var id = Guid.NewGuid();
				var size = await _context.Sizes.OrderBy(s => Guid.NewGuid()).FirstOrDefaultAsync();
				var provices = await _context.Provinces.OrderBy(s => Guid.NewGuid()).Take(2).ToListAsync();
                var company = new User()
				{
					Id = id,
                    Email = $"company{i}@gmail.com",
                    Fullname = $"company{i}",
                    Avatar = AvatarConstant.Default,
                    Password = "123456",
                    Phone = "0123456789",
                    RoleId = RoleEnum.COMPANY,
                    StatusId = UserStatusEnum.ACTIVE,
					Company = new CompanyInfo()
					{
						Id = id,
						Wallpaper = AvatarConstant.Wallpaper,
						Address = address,
						Introduction = $"This is company {i} introduction",
						Website = "Facebook.com",
						Size = size,
						Provinces = provices
                    }
                };
                _context.Users.Add(company);
            }
			rows = await _context.SaveChangesAsync();
		}
		return rows;
	}

	public async Task SeedAsync()
	{
		try
		{
			await InitProvince();
            await InitSize();
			await InitStatus();
			await InitRole();
			await InitUser();
		}
		catch(Exception ex) { }
	}
}
