using EnzojbProd.App.Extensions;
using EnzojbProd.App.Models;
using FluentValidation;

namespace EnzojbProd.App.Validation
{
	public class ProductValidation : AbstractValidator<ProductViewModel>
	{
		public ProductValidation()
		{
			RuleFor(p => p.Ean)
				.NotEmpty()
					.WithMessage("O GINT é obrigatório deve ser numérico")
				.Length(8, 14)
					.WithMessage("O GINT dever ter enter 8 a 14 números");

			RuleFor(p => p.Name)
				.NotEmpty()
					.WithMessage("A descrição é obrigatória")
				.Length(1, 100)
					.WithMessage("A descrição dever ter enter 1 a 100 letras");

			RuleFor(p => p.Stock)
				.InclusiveBetween(0, 99999)
				.WithMessage("A quantidade de estoque deve ser entre 0 é 99999");
		}
	}
}
