namespace EnzojbProd.App.Models
{
	using SQLite;

	[Table("Products")]
	public class ProductViewModel
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		public string Ean { get; set; }

		public string Name { get; set; }

		public decimal Stock { get; set; }

		public virtual string Title => Id > 0 ?
			$"Alterando produto: {Name}" :
			"Incluindo produto";

		public virtual bool DeleteEnabled => Id > 0;
	}
}
