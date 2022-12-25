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
		private readonly SqlLiteConnectionBuilder _connectionBuilder;

		public ProductsRepository(SqlLiteConnectionBuilder connectionBuilder) 
		{
			_connectionBuilder = connectionBuilder;
		}

		internal async Task Init()
		{
			await _connectionBuilder.InitConnectionAsync();
		}

		public async Task<IEnumerable<ProductViewModel>> GetAllAsync(string name = "")
		{
			await Init();
			return string.IsNullOrEmpty(name) ?
				await _connectionBuilder.ConnectionAsync
					.Table<ProductViewModel>().ToListAsync() :
				await _connectionBuilder.ConnectionAsync
					.Table<ProductViewModel>().Where(w => w.Name.ToLower().Contains(name.ToLower())).ToListAsync();
		}

		public async Task<ProductViewModel> GetProductAsync(int id)
		{
			await Init();
			return await _connectionBuilder.ConnectionAsync
				.Table<ProductViewModel>().FirstOrDefaultAsync(f => f.Id.Equals(id));
		}

		public async Task<ProductViewModel> GetProductAsync(string ean)
		{
			await Init();
			return await _connectionBuilder.ConnectionAsync
				.Table<ProductViewModel>().FirstOrDefaultAsync(f => f.Ean.Equals(ean));
		}

		public async Task SaveAsync(ProductViewModel model)
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
			var product = await GetProductAsync(id);
			await _connectionBuilder.ConnectionAsync.RunInTransactionAsync(tran =>
			{
				if (product != null)
				{
					tran.Delete(product);
				}
			});
		}
	}
}
