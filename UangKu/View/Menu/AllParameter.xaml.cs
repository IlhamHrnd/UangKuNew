using UangKu.Model.Base;
using UangKu.ViewModel.Menu;

namespace UangKu.View.Menu;

public partial class AllParameter : ContentPage
{
	private readonly AllParameterVM _vm;
	public AllParameter()
	{
		InitializeComponent();
		_vm = new AllParameterVM(Navigation);
		BindingContext = _vm;
	}
    protected async override void OnAppearing()
    {
        await SessionModel.SessionCheck();

		_vm.LoadData(ParameterModel.ItemDefaultValue.FirstPage, ParameterModel.ItemDefaultValue.Maxresult);
    }

    private void SwipeParameter_Invoked(object sender, EventArgs e)
    {

    }
}