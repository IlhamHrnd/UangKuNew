using UangKu.ViewModel.MasterPage;

namespace UangKu.View.MasterPage;

public partial class MasterAdmin : FlyoutPage
{
	private readonly MasterVM _vm;
	public MasterAdmin()
	{
		InitializeComponent();
		_vm = new MasterVM(Navigation);
		BindingContext = _vm;
	}

    private void Btn_AppStandardReference_Clicked(object sender, EventArgs e)
    {
		_vm.BtnAppStandard_Reference();
    }
}