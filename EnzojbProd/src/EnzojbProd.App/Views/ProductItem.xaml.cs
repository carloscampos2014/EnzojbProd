using EnzojbProd.App.Data;
using EnzojbProd.App.Extensions;
using EnzojbProd.App.Models;
using EnzojbProd.App.Validation;
using System.Globalization;

namespace EnzojbProd.App.Views;

[QueryProperty("Product", "Product")]
public partial class ProductItem : ContentPage
{
	private ProductsRepository _productsRepository;
	private string _currentValue = string.Empty;

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

	private void OnTextChanged(object sender, TextChangedEventArgs e)
	{
		// Check if the input is a number
		if (!string.IsNullOrEmpty(e.NewTextValue) && 
			!decimal.TryParse(e.NewTextValue, NumberStyles.Currency, CultureInfo.CurrentCulture, out _))
		{
			// If the input is not a number, restore the previous value
			((Entry)sender).Text = _currentValue;
		}
		else
		{
			// If the input is a number, update the current value
			_currentValue = e.NewTextValue;
		}
	}
}