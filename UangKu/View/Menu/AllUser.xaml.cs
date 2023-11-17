using UangKu.ViewModel.Menu;

namespace UangKu.View.Menu;

public partial class AllUser : ContentPage
{
	private readonly AllUserVM _vm;
	public AllUser()
	{
		InitializeComponent();
		_vm = new AllUserVM();
		BindingContext = _vm;
	}
}