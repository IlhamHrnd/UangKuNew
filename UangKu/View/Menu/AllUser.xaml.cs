using UangKu.Model.Base;
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
        _vm.PreviousPage_Clicked(ParameterModel.ItemDefaultValue.Maxresult);
    }

    private void Btn_NextPage_Clicked(object sender, EventArgs e)
    {
        _vm.NextPage_Clicked(ParameterModel.ItemDefaultValue.Maxresult);
    }

    private async void Coll_User_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        await _vm.AllUser_PopUp(e);
    }
}