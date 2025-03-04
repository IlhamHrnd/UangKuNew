using UangKu.Model.Base;
using UangKu.ViewModel.Module.UserManagement;

namespace UangKu.View.Module.UserManagement;

public partial class ProfileEdit : ContentPage
{
	private readonly ProfileEditVM _vm;
	public ProfileEdit(string mode)
	{
		InitializeComponent();
		_vm = new ProfileEditVM(mode, Navigation);
		BindingContext = _vm;
	}

    protected async override void OnAppearing()
    {
        await SessionModel.SessionCheck();
        _vm.LoadData();
    }

    private void ButtonUpload_Clicked(object sender, EventArgs e)
    {
        _vm.ImageUpload(AvatarProfile);
    }

    private void PickerProvince_SelectedIndexChanged(object sender, EventArgs e)
    {
        _vm.LoadPicker("prov");
    }

    private void PickerCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        _vm.LoadPicker("city");
    }

    private void PickerDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        _vm.LoadPicker("dis");
    }

    private void PickerSubDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        _vm.LoadPicker("sub");
    }

    private void ButtonSave_Clicked(object sender, EventArgs e)
    {
        _vm.SaveProfile(EntryFirstName.Text, EntryMiddleName.Text, EntryLastName.Text, DateBirth.Date, EntryAddress.Text);
    }
}