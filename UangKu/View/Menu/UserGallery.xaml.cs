using UangKu.Model.Base;
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
}