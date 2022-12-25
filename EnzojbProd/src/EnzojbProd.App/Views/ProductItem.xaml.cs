using EnzojbProd.App.Data;
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
		var validator = new ProductValidation();
		var result = validator.Validate(Product);
		if (!result.IsValid)
		{
			string messageError = string.Empty;
			foreach (var item in result.Errors)
			{
				messageError = $"{messageError}Campo {item.PropertyName} Erro: {item.ErrorMessage}.{Environment.NewLine}";
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