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
                .NotNull()
                .WithMessage("请输入整数");

            RuleFor(v => v.Second)
                .NotNull()
                .WithMessage("请输入整数");
        }
    }
}