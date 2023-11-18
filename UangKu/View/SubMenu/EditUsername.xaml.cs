using UangKu.Model.Base;
using UangKu.ViewModel.SubMenu;

namespace UangKu.View.SubMenu;

public partial class EditUsername : ContentPage
{
	private readonly EditUsernameVM _vm;
	public EditUsername()
	{
		InitializeComponent();
		_vm = new EditUsernameVM();
		BindingContext = _vm;
	}
    protected async override void OnAppearing()
    {
        await SessionModel.SessionCheck();
		_vm.LoadData(Avt_User, CB_IsActive);
    }

    private async void Btn_Update_Clicked(object sender, EventArgs e)
    {
		await _vm.UpdateUsername_Click(Pic_Access, CB_IsActive);
    }
}