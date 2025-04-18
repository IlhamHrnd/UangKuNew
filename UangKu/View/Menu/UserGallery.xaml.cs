using UangKu.Model.Base;
using UangKu.Model.Session;
using UangKu.ViewModel.Menu;

namespace UangKu.View.Menu;

public partial class UserGallery : ContentPage
{
	private readonly UserGalleryVM _vm;
    public UserGallery()
	{
		InitializeComponent();
		_vm = new UserGalleryVM();
		BindingContext = _vm;
	}
    protected async override void OnAppearing()
    {
        await SessionModel.SessionCheck();
		await _vm.LoadData();
    }

    private async void Bar_AddItem_Clicked(object sender, EventArgs e)
    {
        await _vm.UploadPicture_PopUp();
    }

    private void Btn_PreviousPage_Clicked(object sender, EventArgs e)
    {
        _vm.NextPreviousPage_Clicked(AppParameter.MaxResult, false);
    }

    private void Btn_NextPage_Clicked(object sender, EventArgs e)
    {
        _vm.NextPreviousPage_Clicked(AppParameter.MaxResult, true);
    }

    private async void Btn_DeletePicture_Clicked(object sender, EventArgs e)
    {
        await _vm.DeleteUserPicture_Clicked();
    }
}