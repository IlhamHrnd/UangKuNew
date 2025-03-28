using Microcharts;
using UangKu.Model.Base;
using UangKu.ViewModel.Menu;

namespace UangKu.View.Menu;

public partial class HomePage : ContentPage
{
	private readonly HomeVM _vm;
	public HomePage()
	{
		InitializeComponent();
		_vm = new HomeVM(Navigation);
		BindingContext = _vm;
	}
    protected async override void OnAppearing()
    {
		await SessionModel.SessionCheck();
		_vm.LoadDataPerson(Chart_Transaction);
    }
}