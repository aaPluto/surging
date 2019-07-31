using FluentValidation;
using Hl.Identity.IApplication.Authorization.Dtos;
using Surging.Core.CPlatform.Ioc;
using System;

namespace Hl.Identity.IApplication.Authorization.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterInput>, ITransientDependency
    {
        public RegisterValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("用户名不允许为空").Matches("^[a-zA-Z0-9_-]{4,16}$").WithMessage("用户名格式不正确");
            RuleFor(x => x.Password).NotEmpty().NotNull().Length(6, 18).WithMessage("密码格式不正确");
            RuleFor(x => x.RepeatedPassword).Must((m, p) => p == m.Password).WithMessage("密码不一致");
            RuleFor(x => x.Phone).Matches("^((13[0-9])|(14[5,7])|(15[0-3,5-9])|(17[0,3,5-8])|(18[0-9])|166|198|199|(147))\\d{8}$").WithMessage("手机号码格式不正确");
            RuleFor(x => x.Email).Matches("^\\w+([\\.-]?\\w+)*@\\w+([\\.-]?\\w+)*(\\.\\w{2,3})+$").WithMessage("Email格式不正确");
            
        }
    }
}
