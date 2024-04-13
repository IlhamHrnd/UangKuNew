using UangKu.Model.Base;
using UangKu.Model.Session;
using UangKu.ViewModel.Menu;

namespace UangKu.View.Menu;

public partial class AllUser : ContentPage
{
	private readonly AllUserVM _vm;
	public AllUser()
	{
		InitializeComponent();
		_vm = new AllUserVM(Navigation);
		BindingContext = _vm;
	}
    protected async override void OnAppearing()
    {
        await SessionModel.SessionCheck();

        _vm.LoadData();
    }

    private void Btn_PreviousPage_Clicked(object sender, EventArgs e)
    {
        _vm.NextPreviousPage_Clicked(AppParameter.MaxResult, false);
    }

    private void Btn_NextPage_Clicked(object sender, EventArgs e)
    {
        _vm.NextPreviousPage_Clicked(AppParameter.MaxResult, true);
    }

    private async void Coll_User_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        await _vm.AllUser_PopUp(e);
    }
}