using UangKu.ViewModel.Index;

namespace UangKu.View.Index;

public partial class SignUpPage : ContentPage
{
	private readonly SignUpVM _vm;
	public SignUpPage()
	{
		InitializeComponent();
		_vm = new SignUpVM();
		BindingContext = _vm;
	}
    protected override void OnAppearing()
    {
		_vm.LoadData();
    }

    private void Ent_SignUp_Clicked(object sender, EventArgs e)
    {
		_vm.SignUp_User(Ent_Username, Ent_Password, Ent_ConfirmPass, Ent_Email, Pic_Sex);
    }
}