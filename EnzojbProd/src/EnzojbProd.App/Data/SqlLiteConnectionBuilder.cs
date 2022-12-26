namespace EnzojbProd.App.Data
{
	using EnzojbProd.App.Models;
	using SQLite;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public class SqlLiteConnectionBuilder
    {
		private SQLiteAsyncConnection _connection;

		public SqlLiteConnectionBuilder() { }

		public SQLiteAsyncConnection ConnectionAsync => _connection;

		public async Task InitConnectionAsync()
		{
			if (_connection is not null)
				return;

			_connection = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
			await _connection.ExecuteAsync("PRAGMA foreignkeys = ON");

			await _connection.CreateTableAsync<ProductViewModel>();
			await _connection.CreateTableAsync<InventoryViewModel>();
		}
	}
}
