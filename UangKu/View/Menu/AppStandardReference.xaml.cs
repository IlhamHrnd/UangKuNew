using CommunityToolkit.Maui.Views;
using UangKu.Model.Base;
using UangKu.Model.Session;
using UangKu.ViewModel.Menu;

namespace UangKu.View.Menu;

public partial class AppStandardReference : ContentPage
{
	private readonly AppStandardReferenceVM _vm;
	public AppStandardReference()
	{
		InitializeComponent();
		_vm = new AppStandardReferenceVM(Navigation);
		BindingContext = _vm;
	}
    protected override async void OnAppearing()
    {
        await SessionModel.SessionCheck();

        _vm.LoadData(ParameterModel.ItemDefaultValue.FirstPage, AppParameter.MaxResult);
    }

    private void Btn_NextPage_Clicked(object sender, EventArgs e)
    {
		_vm.NextPreviousPage_Click(AppParameter.MaxResult, true);
    }

    private void Btn_PreviousPage_Clicked(object sender, EventArgs e)
    {
        _vm.NextPreviousPage_Click(AppParameter.MaxResult, false);
    }

    private async void Coll_AppStandardReference_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        await _vm.AppStandardReferenceItem_PopUp(e);
    }

    private async void Bar_AddItem_Clicked(object sender, EventArgs e)
    {
        await _vm.AddASRItem_ToolBar();
    }
}