using EnzojbProd.App.Data;
using EnzojbProd.App.Views;

namespace EnzojbProd.App
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				});
			builder.Services.AddSingleton<Home>();
			builder.Services.AddSingleton<ProductList>();
			builder.Services.AddTransient<ProductItem>();
			builder.Services.AddSingleton<InventoryList>();
			builder.Services.AddSingleton<SqlLiteConnectionBuilder>();
			builder.Services.AddSingleton<ProductsRepository>();
			builder.Services.AddSingleton<InventoryRepository>();

			return builder.Build();
		}
	}
}