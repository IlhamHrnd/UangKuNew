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
        _vm.LoadData(Avt_Profile, Ent_FirstName, Ent_MiddleName, Ent_LastName, Pic_PlaceOfBirth);
    }

    private void Btn_UploadPhoto_Clicked(object sender, EventArgs e)
    {
        _vm.UploadPhoto_Click(Avt_Profile);
    }
}