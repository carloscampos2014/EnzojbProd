namespace EnzojbProd.App
{
	using System.Runtime.InteropServices;

	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new AppShell();
		}
	}
}