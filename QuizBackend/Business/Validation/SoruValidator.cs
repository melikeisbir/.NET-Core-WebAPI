using Entity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation
{
    public class SoruValidator:AbstractValidator<Soru>
    {
        public SoruValidator()
        {
            RuleFor(x => x.SoruText).NotEmpty().WithMessage("Soru kısmı boş geçilemez.");
            RuleFor(x => x.KonuId).NotEmpty().WithMessage("Konu Id boş geçilemez.");
            RuleFor(x => x.DogruCevap).NotEmpty().WithMessage("Cevap kısmı boş geçilemez.");
        }
    }
}
