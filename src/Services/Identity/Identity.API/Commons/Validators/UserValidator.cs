using FluentValidation;

namespace Identity.API.Commons.Validators;

public static class UserValidator
{
	public static IRuleBuilderOptions<T, string> EmailRule<T>(this IRuleBuilder<T, string> ruleBuilder)
	{
		return ruleBuilder
			.NotEmpty().WithMessage("Email không được để trống")
			.EmailAddress().WithMessage("Invalid không đúng định dạng");
	}

	public static IRuleBuilderOptions<T, string?> PasswordRule<T>(this IRuleBuilder<T, string?> ruleBuilder)
	{
		return ruleBuilder
			.NotEmpty().WithMessage("Password không được để trống")
			.Matches(@"^[a-z0-9]*$").WithMessage("Password chỉ có thể chứa số và chữ thường")
			.Matches(@"^\S*$").WithMessage("Password không được chứa khoảng trắng")
			.Length(6, 15).WithMessage("Password phải từ 6 đến 15 kí tự");
	}

	public static IRuleBuilderOptions<T, string> PhoneRule<T>(this IRuleBuilder<T, string> ruleBuilder)
	{
		return ruleBuilder
			.NotEmpty().WithMessage("Số điện thoại Không được để trống")
			.Matches(@"^0\d{9}$").WithMessage("Số điện thoại phải bắt đầu bằng 0 và có 10 kí tự");
	}

	public static IRuleBuilderOptions<T, string> FullnameRule<T>(this IRuleBuilder<T, string> ruleBuilder)
	{
		return ruleBuilder
			.NotEmpty().WithMessage("Tên hiện thị không được để trống");
	}

	public static IRuleBuilderOptions<T, string> AddressRule<T>(this IRuleBuilder<T, string> ruleBuilder)
	{
		return ruleBuilder
			.NotEmpty().WithMessage("Địa chỉ không được để trống");
	}
}
