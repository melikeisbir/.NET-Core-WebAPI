using Entity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation
{
    public class DersValidator : AbstractValidator<Ders>
    {
        public DersValidator()
        {
            RuleFor(x => x.DersAdi).NotEmpty().WithMessage("Ders adı boş geçilemez.");
        }
    }
}
