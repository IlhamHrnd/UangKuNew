using UangKu.ViewModel.Index;

namespace UangKu.View.Index;

public partial class ForgetPasswordPage : ContentPage
{
	private readonly ForgetPasswordVM _vm;
	public ForgetPasswordPage()
	{
		InitializeComponent();
		_vm = new ForgetPasswordVM();
		BindingContext = _vm;
	}

    private void Btn_Update_Clicked(object sender, EventArgs e)
    {
		_vm.BtnUpdate_User(Ent_Username, Ent_Email, Ent_Password, Ent_ConfirmPass);
    }
}