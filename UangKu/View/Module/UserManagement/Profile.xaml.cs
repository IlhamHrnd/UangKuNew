using UangKu.Model.Base;
using UangKu.ViewModel.Module.UserManagement;

namespace UangKu.View.Module.UserManagement;

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
		_vm.LoadData();
    }

    private void ButtonMode_Clicked(object sender, EventArgs e)
    {
		_vm.NavigationPage();
    }
}