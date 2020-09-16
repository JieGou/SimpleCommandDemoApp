using FluentValidation;
using SimpleCommandDemoApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCommandDemoApp
{
    public class CalculatorValidator : AbstractValidator<CalculatorViewModel>
    {
        public CalculatorValidator()
        {
            RuleFor(v => v.First)
                .NotEmpty()
                .WithMessage("输入值不能为空")
                .NotNull()
                .Must(s => int.TryParse(s, out _))
                .WithMessage("请输入整数");

            RuleFor(v => v.Second)
                .NotEmpty()
                .WithMessage("输入值不能为空")
                .NotNull()
                .Must(s => int.TryParse(s, out _))
                .WithMessage("请输入整数");
        }
    }
}