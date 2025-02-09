using UangKu.Model.Base;
using UangKu.ViewModel.Module.UserManagement;

namespace UangKu.View.Module.UserManagement;

public partial class ProfileEdit : ContentPage
{
	private readonly ProfileEditVM _vm;
	public ProfileEdit(string mode)
	{
		InitializeComponent();
		_vm = new ProfileEditVM(mode);
		BindingContext = _vm;
	}

    protected async override void OnAppearing()
    {
        await SessionModel.SessionCheck();
        _vm.LoadData();
    }

    private void ButtonUpload_Clicked(object sender, EventArgs e)
    {
        _vm.ImageUpload(AvatarProfile.ImageSource);
    }
}