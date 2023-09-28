using UangKu.ViewModel.Menu;

namespace UangKu.View.Menu;

public partial class HomePage : ContentPage
{
	private readonly HomeVM _vm;
	public HomePage()
	{
		InitializeComponent();
		_vm = new HomeVM();
		BindingContext = _vm;
	}
}