namespace Identity.API.Data.Seeding;

public class DataContextInitializer : IDataContextInitializer
{
	private readonly DataContext _context;
	public DataContextInitializer(DataContext context)
	{
		_context = context;
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

			for(int i = 1; i <= 10; i++)
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

                var company = new User()
                {
                    Email = $"company{i}@gmail.com",
                    Fullname = $"company{i}",
                    Avatar = AvatarConstant.Default,
                    Password = "123456",
                    Phone = "0123456789",
                    RoleId = RoleEnum.COMPANY,
                    StatusId = UserStatusEnum.ACTIVE
                };
                _context.Users.Add(customer);
            }
			rows = await _context.SaveChangesAsync();
		}
		return rows;
	}

	public async Task SeedAsync()
	{
		try
		{
			await InitStatus();
			await InitRole();
			await InitUser();
		}
		catch(Exception ex) { }
	}
}
