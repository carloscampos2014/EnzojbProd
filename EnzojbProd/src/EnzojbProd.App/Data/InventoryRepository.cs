namespace EnzojbProd.App.Data
{
	using EnzojbProd.App.Models;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public class InventoryRepository
    {
		private readonly SqlLiteConnectionBuilder _connectionBuilder;

		public InventoryRepository(SqlLiteConnectionBuilder connectionBuilder) 
		{
			_connectionBuilder = connectionBuilder;
		}

		internal async Task Init()
		{
			await _connectionBuilder.InitConnectionAsync();
		}

		public async Task<IEnumerable<InventoryViewModel>> GetAllAsync()
		{
			await Init();
			return await _connectionBuilder.ConnectionAsync
				.Table<InventoryViewModel>().ToListAsync();	
		}

		public async Task<InventoryViewModel> GetInventoryAsync(int id)
		{
			await Init();
			return await _connectionBuilder.ConnectionAsync
				.Table<InventoryViewModel>().FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task SaveAsync(InventoryViewModel model)
		{
			await Init();
			await _connectionBuilder.ConnectionAsync.RunInTransactionAsync(tran =>
			{
				if (model.Id <= 0)
				{
					tran.Insert(model);
				}
				else
				{
					tran.Update(model);
				}
			});
		}

		public async Task DeleteAsync(int id)
		{
			await Init();
			var inventory = await GetInventoryAsync(id);
			await _connectionBuilder.ConnectionAsync.RunInTransactionAsync(tran =>
			{
				if (inventory != null)
				{
					tran.Delete(inventory);
				}
			});
		}

		public async Task DeleteAllAsync()
		{
			await Init();
			await _connectionBuilder.ConnectionAsync.RunInTransactionAsync(tran =>
			{
				tran.DeleteAll<InventoryViewModel>();
			});
		}
	}
}
