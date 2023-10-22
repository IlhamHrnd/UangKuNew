using UangKu.Model.Base;
using UangKu.ViewModel.Menu;

namespace UangKu.View.Menu;

public partial class Profile : ContentPage
{
	private readonly ProfileVM _vm;
	public Profile()
	{
		InitializeComponent();
		_vm = new ProfileVM();
		BindingContext = _vm;
	}

    protected async override void OnAppearing()
    {
        await SessionModel.SessionCheck();
		await _vm.LoadData(Avt_Profile);
    }
}