using Entity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation
{
    public class KonuValidator : AbstractValidator<Konu>
    {
        public KonuValidator()
        {
            RuleFor(x => x.KonuAdi).NotEmpty().WithMessage("Konu adı boş geçilemez.");
            RuleFor(x => x.DersId).NotEmpty().WithMessage("Ders Id boş geçilemez.");
        }
    }
}
