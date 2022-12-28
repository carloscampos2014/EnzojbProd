namespace EnzojbProd.App.Models
{
	using EnzojbProd.App.Enums;
	using EnzojbProd.App.Extensions;
	using SQLite;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	[Table("Inventories")]
	public class InventoryViewModel
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		public InventoryType Type { get; set; }

		public InventoryStatus Status { get; set; }

		public DateTime? StartDate { get; set; }

		public DateTime? EndDate { get; set;}

		public virtual string TypeStatus => $"{Type.Description()}-{Status.Description()}";
	}
}
