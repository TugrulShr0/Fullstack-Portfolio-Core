namespace BusinessLayer.ValidationRules;

using EntityLayer.Concrete;
using FluentValidation;

public class PortfolioValidator : AbstractValidator<Portfolio>
{
    public PortfolioValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Proje Adını Boş Geçemezsiniz");
        RuleFor(x => x.Name).MinimumLength(3).WithMessage("Proje Adı en az 3 karakter olmalıdır");
        RuleFor(x => x.Price).NotEmpty().WithMessage("Fiyat Alanı Boş Geçilemez");
        RuleFor(x => x.Value).InclusiveBetween(0, 100).WithMessage("Tamamlanma Oranı 0 ile 100 arasında olmalıdır");
    }
}
