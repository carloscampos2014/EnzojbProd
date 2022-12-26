using EnzojbProd.App.Data;
using EnzojbProd.App.Extensions;
using EnzojbProd.App.Models;
using EnzojbProd.App.Validation;

namespace EnzojbProd.App.Views;

[QueryProperty("Product", "Product")]
public partial class ProductItem : ContentPage
{
	ProductsRepository _productsRepository;

	public ProductViewModel Product
	{
		get => BindingContext as ProductViewModel;
		set => BindingContext = value;
	}

	public ProductItem(ProductsRepository productsRepository)
	{
		InitializeComponent();
		_productsRepository = productsRepository;
	}

	private async void OnSaveClicked(object sender, EventArgs e)
	{
		Product.Ean = Product.Ean.SoNumeros(false);
		var validator = new ProductValidation();
		var result = validator.Validate(Product);
		if (!result.IsValid)
		{
			string messageError = result.Errors.Count == 1 ?
				$"Erro encontrado:{Environment.NewLine}":
				$"Erros encontrados:{Environment.NewLine}";
			foreach (var item in result.Errors)
			{
				messageError = $"{messageError}  {item.ErrorMessage}.{Environment.NewLine}";
			}
			await DisplayAlert("Dados inválidos", messageError, "OK");
		}
		else
		{
			await _productsRepository.SaveAsync(Product);
			await Shell.Current.GoToAsync("..");
		}
	}

	private async void OnDeleteClicked(object sender, EventArgs e)
	{
		if (!await DisplayAlert("Atenção", $"Deseja realmente excluir o produto:{Environment.NewLine}{Product.Name}?", "Não", "Sim"))
		{
			await _productsRepository.DeleteAsync(Product.Id);
			await Shell.Current.GoToAsync("..");
		}
	}

	private async void OnCancelClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("..");
	}

	protected override bool OnBackButtonPressed()
	{
		return true;
	}
}