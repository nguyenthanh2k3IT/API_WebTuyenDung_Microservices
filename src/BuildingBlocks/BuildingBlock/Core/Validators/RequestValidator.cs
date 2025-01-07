using FluentValidation;

namespace BuildingBlock.Core.Validators;

public static class RequestValidator
{
    public static IRuleBuilderOptions<T, string> FieldNotEmpty<T>(this IRuleBuilder<T, string> ruleBuilder,string entity)
    {
        return ruleBuilder.NotEmpty().WithMessage($"{entity} không được để trống");
    }

    public static IRuleBuilderOptions<T, Guid?> RequiredID<T>(this IRuleBuilder<T, Guid?> ruleBuilder)
    {
        return ruleBuilder.NotEmpty().WithMessage($"Không tìm thấy ID");
    }
}
