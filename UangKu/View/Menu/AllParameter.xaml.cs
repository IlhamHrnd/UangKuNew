using UangKu.Model.Base;
using UangKu.Model.Session;
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

		_vm.LoadData(ParameterModel.ItemDefaultValue.FirstPage, Converter.StringToInt(AppParameter.MaxResult, ParameterModel.AppParameterDefault.Maxresult));
    }

    private async void SwipeParameter_Invoked(object sender, EventArgs e)
    {
        await _vm.SwipeItem_Invoked(sender, ParameterModel.ItemDefaultValue.EditFile);
    }

    private async void Bar_AddItem_Clicked(object sender, EventArgs e)
    {
        await _vm.AddAppParameter_ToolBar();
    }
}