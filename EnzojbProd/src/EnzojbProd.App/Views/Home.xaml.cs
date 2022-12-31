using EnzojbProd.App.Data;
using EnzojbProd.App.Models;

namespace EnzojbProd.App.Views;

public partial class Home : ContentPage
{
	private readonly ProductsRepository _productsRepository;

	internal async Task LoadData()
	{
		var products = await _productsRepository.GetAllAsync();
		HomeViewModel homeViewData = new HomeViewModel();
		homeViewData.RegisteredProducts = products.Count();
		homeViewData.StockProducts = products.Sum(x => x.Stock);
		BindingContext = homeViewData;
	}

	public Home(ProductsRepository productsRepository,
		InventoryRepository inventoryRepository)
	{
		InitializeComponent();
		_productsRepository = productsRepository;
	}

	protected override async void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);
		await LoadData();	
	}
}