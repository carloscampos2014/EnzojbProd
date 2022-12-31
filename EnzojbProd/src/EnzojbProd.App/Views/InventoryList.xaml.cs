using EnzojbProd.App.Data;
using EnzojbProd.App.Models;
using System.Collections.ObjectModel;

namespace EnzojbProd.App.Views;

public partial class InventoryList : ContentPage
{
	private readonly InventoryRepository _inventoryRepository;

	public ObservableCollection<InventoryViewModel> Inventories { get; set; } = new();

	internal async Task LoadData()
	{
		var itens = await _inventoryRepository.GetAllAsync();
		MainThread.BeginInvokeOnMainThread(() =>
		{
			Inventories.Clear();
			foreach (var item in itens)
			{
				Inventories.Add(item);
			}
		});
	}

	public InventoryList(InventoryRepository inventoryRepository)
	{
		InitializeComponent();
		BindingContext = this;
		_inventoryRepository = inventoryRepository;
	}

	private async void OnDeleteAllClicked(object sender, EventArgs e)
	{
		if (!await DisplayAlert("Atenção", $"Deseja realmente excluir todos inventários?", "Não", "Sim"))
		{
			await _inventoryRepository.DeleteAllAsync();
		}
	}

	protected override async void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);
		await LoadData();
	}
}