using UangKu.Model.Base;
using UangKu.ViewModel.Menu;

namespace UangKu.View.Menu;

public partial class Profile : ContentPage
{
	private readonly ProfileVM _vm;
	public Profile()
	{
		InitializeComponent();
		_vm = new ProfileVM(Navigation);
		BindingContext = _vm;
	}

    protected async override void OnAppearing()
    {
        await SessionModel.SessionCheck();
		await _vm.LoadData(Avt_Profile);
    }

    private async void Btn_Edit_Clicked(object sender, EventArgs e)
    {
		await _vm.EditProfile_Click();
    }
}