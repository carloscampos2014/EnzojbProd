namespace EnzojbProd.App.Data
{
	using EnzojbProd.App.Models;
	using SQLite;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public class ProductsRepository
	{
		private SQLiteAsyncConnection connection;

		public ProductsRepository() { }

		internal async Task Init()
		{
			if (connection is not null)
				return;

			connection = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);

			await connection.CreateTableAsync<ProductViewModel>();
		}

		public async Task<IEnumerable<ProductViewModel>> GetAllAsync(string name = "")
		{
			await Init();
			return string.IsNullOrEmpty(name) ?
				await connection.Table<ProductViewModel>().ToListAsync() :
				await connection.Table<ProductViewModel>().Where(w => w.Name.ToLower().Contains(name.ToLower())).ToListAsync();
		}

		public async Task<ProductViewModel> GetProductAsync(int id)
		{
			await Init();
			return await connection.Table<ProductViewModel>().FirstOrDefaultAsync(f => f.Id.Equals(id));
		}

		public async Task<ProductViewModel> GetProductAsync(string ean)
		{
			await Init();
			return await connection.Table<ProductViewModel>().FirstOrDefaultAsync(f => f.Ean.Equals(ean));
		}

		public async Task SaveAsync(ProductViewModel model)
		{
			await Init();
			await connection.RunInTransactionAsync(tran =>
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
			var product = await GetProductAsync(id);
			await connection.RunInTransactionAsync(tran =>
			{
				if (product != null)
				{
					tran.Delete(product);
				}
			});
		}
	}
}
