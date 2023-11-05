using UangKu.ViewModel.MasterPage;

namespace UangKu.View.MasterPage;

public partial class MasterUser : Shell
{
    private readonly MasterVM _vm;
    public MasterUser()
	{
		InitializeComponent();
        _vm = new MasterVM();
        BindingContext = _vm;
    }
    private void Btn_LogOut_Clicked(object sender, EventArgs e)
    {
        _vm.BtnLogOut_Clicked();
    }
}