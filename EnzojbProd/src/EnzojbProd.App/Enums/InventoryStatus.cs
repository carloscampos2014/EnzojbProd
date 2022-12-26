namespace EnzojbProd.App.Enums
{
	using System.ComponentModel;

	public enum InventoryStatus
    {
        [Description("Criado")]
        Created = 0,

		[Description("Iniciado")]
		Started = 1,

		[Description("Conferência")]
		InConference = 2,

		[Description("Finalizado")]
		Finished = 3,
	}
}
