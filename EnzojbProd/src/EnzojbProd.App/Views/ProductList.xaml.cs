using EnzojbProd.App.Data;
using EnzojbProd.App.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EnzojbProd.App.Views;

public partial class ProductList : ContentPage
{
	private readonly ProductsRepository _productsRepository;
	public ObservableCollection<ProductViewModel> Produtos { get; set; } = new();

	internal async Task OpenProductItem(ProductViewModel product)
	{
		await Shell.Current.GoToAsync(nameof(ProductItem), true, new Dictionary<string, object>
		{
			["Product"] = product
		});
	}

	internal async Task LoadData(string query)
	{
		var items = await _productsRepository.GetAllAsync(query);
		MainThread.BeginInvokeOnMainThread(() =>
		{
			Produtos.Clear();
			foreach (var item in items)
				Produtos.Add(item);
		});
	}

	public ProductList(ProductsRepository productsRepository)
	{
		InitializeComponent();
		BindingContext = this;
		_productsRepository = productsRepository;
	}

	private async void OnTextChanged(object sender, EventArgs e)
	{
		SearchBar searchBar = (SearchBar)sender;
		await LoadData(searchBar.Text);
	}

	private async void OnNewClicked(object sender, EventArgs e)
	{
		await OpenProductItem(new ProductViewModel());
	}

	private async void OnDeleteAllClicked(object sender, EventArgs e)
	{
		if (!await DisplayAlert("Atenção", $"Deseja realmente excluir todos produtos?", "Não", "Sim"))
		{
			await _productsRepository.DeleteAllAsync();
			searchBar.Text= string.Empty;	
		}
	}

	private async void SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (e.CurrentSelection.FirstOrDefault() is not ProductViewModel product)
			return;
		await OpenProductItem(product);
	}

	protected override async void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);
		await LoadData(string.Empty);
	}
}