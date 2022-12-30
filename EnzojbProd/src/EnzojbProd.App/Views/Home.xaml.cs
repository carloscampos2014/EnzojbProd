using EnzojbProd.App.Data;
using EnzojbProd.App.Models;

namespace EnzojbProd.App.Views;

public partial class Home : ContentPage
{
	private readonly ProductsRepository _productsRepository;
	private readonly InventoryRepository _inventoryRepository;

	internal async Task LoadData()
	{
		var products = await _productsRepository.GetAllAsync();
		var inventory = await _inventoryRepository.GetAllAsync();
		HomeViewModel homeViewData = new HomeViewModel();
		homeViewData.RegisteredProducts = products.Count();
		homeViewData.StockProducts = products.Sum(x => x.Stock);
		homeViewData.CreatedInventories = inventory.Count(x => x.Status == Enums.InventoryStatus.Created);
		homeViewData.StartedInventories = inventory.Count(x => x.Status == Enums.InventoryStatus.Started);
		homeViewData.FinishedInventories = inventory.Count(x => x.Status == Enums.InventoryStatus.Finished);
		homeViewData.Inventories = inventory.Where(x => x.Status == Enums.InventoryStatus.Started);
		BindingContext = homeViewData;
	}

	public Home(ProductsRepository productsRepository,
		InventoryRepository inventoryRepository)
	{
		InitializeComponent();
		_productsRepository = productsRepository;
		_inventoryRepository = inventoryRepository;
	}

	protected override async void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);
		await LoadData();	
	}
}