namespace EnzojbProd.App
{
	using EnzojbProd.App.Views;
	using System.Windows.Input;

	public partial class AppShell : Shell
	{
		public ICommand ExitCommand => new Command(() => App.Current.Quit());

		public AppShell()
		{
			InitializeComponent();
			BindingContext = this;
			Routing.RegisterRoute(nameof(ProductItem), typeof(ProductItem));
		}

		protected override bool OnBackButtonPressed()
		{
			return true;
		}

	}
}