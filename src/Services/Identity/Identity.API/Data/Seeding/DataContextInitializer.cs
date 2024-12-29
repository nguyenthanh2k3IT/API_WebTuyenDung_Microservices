namespace Identity.API.Data.Seeding;

public class DataContextInitializer : IDataContextInitializer
{
	private readonly DataContext _context;
	public DataContextInitializer(DataContext context)
	{
		_context = context;
	}

    public class ProvinceOpenAPI
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Type { get; set; }
        public string NameWithType { get; set; }
        public string Path { get; set; }
        public string PathWithType { get; set; }
        public string Districts { get; set; } // Optional, tùy thuộc vào response
    }

    public async Task<int> InitProvince()
    {
		int rows = 0;
		if (!_context.Provinces.Any())
		{

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
			await InitSize();
			await InitStatus();
			await InitRole();
			await InitUser();
		}
		catch(Exception ex) { }
	}
}
