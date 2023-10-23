using UangKu.Model.Base;
using UangKu.ViewModel.SubMenu;

namespace UangKu.View.SubMenu;

public partial class EditProfile : ContentPage
{
    private readonly EditProfileVM _vm;
	public EditProfile()
	{
		InitializeComponent();
        _vm = new EditProfileVM();
        BindingContext = _vm;
	}

    protected async override void OnAppearing()
    {
        await SessionModel.SessionCheck();
        _vm.LoadData(Avt_Profile, Ent_FirstName, Ent_MiddleName, Ent_LastName, Pic_PlaceOfBirth,
            Ent_StreetName, Pic_Provinces, Pic_Cities, Pic_Districts, Pic_Subdistricts, Ent_PostalCode);
    }

    private void Btn_UploadPhoto_Clicked(object sender, EventArgs e)
    {
        _vm.UploadPhoto_Click(Avt_Profile);
    }

    private async void Pic_Provinces_SelectedIndexChanged(object sender, EventArgs e)
    {
        await _vm.PickerProvinces_Changed(Pic_Provinces);
    }

    private async void Pic_Cities_SelectedIndexChanged(object sender, EventArgs e)
    {
        await _vm.PickerCity_Changed(Pic_Cities);
    }

    private async void Pic_Districts_SelectedIndexChanged(object sender, EventArgs e)
    {
        await _vm.PickerDistrict_Changed(Pic_Districts);
    }

    private async void Pic_Subdistricts_SelectedIndexChanged(object sender, EventArgs e)
    {
        await _vm.PickerSubdistrict_Changed(Pic_Subdistricts, Ent_PostalCode);
    }

    private async void Btn_UpdateProfile_Clicked(object sender, EventArgs e)
    {
        await _vm.SaveProfile_Clicked();
    }
}