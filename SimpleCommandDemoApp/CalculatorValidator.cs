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
            int result = default(int);
            RuleFor(v => v.First)
                .NotEmpty()
                .NotNull()
                .Must(s => int.TryParse(s, out result))
                .WithMessage("请输入整数");

            RuleFor(v => v.Second)
                .NotEmpty()
                .NotNull()
                .Must(s => int.TryParse(s, out result))
                .WithMessage("请输入整数");
        }
    }
}