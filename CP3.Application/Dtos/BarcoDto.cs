using CP3.Domain.Interfaces.Dtos;
using FluentValidation;

namespace CP3.Application.Dtos
{
    public class BarcoDto : IBarcoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public int Ano { get; set; }
        public double Tamanho { get; set; }

        public void Validate()
        {
            new BarcoDtoValidation().ValidateAndThrow(this);
        }
    }

    internal class BarcoDtoValidation : AbstractValidator<BarcoDto>
    {
        public BarcoDtoValidation()
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage("O nome é obrigatório");
            RuleFor(x => x.Modelo).NotEmpty().WithMessage("O modelo é obrigatório");
            RuleFor(x => x.Ano).InclusiveBetween(1900, 2100).WithMessage("Ano deve estar entre 1900 e 2100");
            RuleFor(x => x.Tamanho).GreaterThan(0).WithMessage("O tamanho deve ser positivo");
        }
    }
}