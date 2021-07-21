using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator: AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p=>p.ProductName).NotEmpty().WithErrorCode("2020");
            RuleFor(p => p.ProductName).Length(2,30).WithErrorCode("2021");
            RuleFor(p => p.UnitPrice).NotEmpty().WithErrorCode("2022");
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(1).WithErrorCode("2023");
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p=>p.CategoryId==1).WithErrorCode("2024");
            RuleFor(p => p.ProductName).Must(StartWithA).WithErrorCode("2025");
        }

        private bool StartWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
