namespace EnzojbProd.App.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public class HomeViewModel
	{
		public decimal RegisteredProducts { get; set; }

		public decimal StockProducts { get; set; }

		public decimal CreatedInventories { get; set; }

		public decimal StartedInventories { get; set; }

		public decimal FinishedInventories { get; set; }

		public IEnumerable<InventoryViewModel> Inventories { get; set; } = new List<InventoryViewModel>();
	}
}
