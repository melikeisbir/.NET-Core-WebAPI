using Entity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation
{
    public class KullaniciValidator : AbstractValidator<Kullanici>
    {
        public KullaniciValidator()
        {
            RuleFor(x => x.KullaniciAdi).NotEmpty().WithMessage("Kullanici adı boş geçilemez.");
            RuleFor(x => x.Eposta).NotEmpty().WithMessage("Eposta boş geçilemez.");
            RuleFor(x => x.Sifre).NotEmpty().WithMessage("Şifre boş geçilemez.").MinimumLength(8);
        }
    }
}

