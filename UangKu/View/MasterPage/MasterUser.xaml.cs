using UangKu.ViewModel.MasterPage;

namespace UangKu.View.MasterPage;

public partial class MasterUser : ContentPage
{
	private readonly MasterVM _vm;
	public MasterUser()
	{
		InitializeComponent();
		_vm = new MasterVM(Navigation);
		BindingContext = _vm;
	}
}