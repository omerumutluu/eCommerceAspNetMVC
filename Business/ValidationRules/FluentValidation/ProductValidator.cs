using Entity.Concrete;
using Entity.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;


using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("Ürün ismi boş geçilemez.");
            RuleFor(x => x.ProductName).MinimumLength(3).WithMessage("Ürün ismi 3 karekterden az olamaz.");
            RuleFor(x => x.ProductName).MaximumLength(50).WithMessage("Ürün ismi 50 karekterden yüksek olamaz.");

            RuleFor(x => x.ProductThumbnail).NotNull().WithMessage("Ürün resmi boş geçilemez.");

            RuleFor(x => x.UnitPrice).NotNull().WithMessage("Ürün fiyatı boş geçilemez.");
            RuleFor(x => x.UnitPrice).GreaterThan(0).WithMessage("Ürün fiyatı 0 TL'den büyük olmalıdır.");
            RuleFor(x => x.UnitPrice).LessThan(150000).WithMessage("Ürün fiyatı 150.000 TL'den yüksek olamaz.");

            RuleFor(x => x.UnitsInStock).NotNull().WithMessage("Ürün stoğu boş geçilemez.");
            RuleFor(x => x.UnitsInStock).GreaterThan(0).WithMessage("Ürün stoğu 0'dan büyük olmalıdır.");

        }
    }
}
