using UangKu.ViewModel.MasterPage;

namespace UangKu.View.MasterPage;

public partial class MasterAdmin : Shell
{
	private readonly MasterVM _vm;
	public MasterAdmin()
	{
		InitializeComponent();
		_vm = new MasterVM();
		BindingContext = _vm;
	}
}