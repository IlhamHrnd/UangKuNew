using CommunityToolkit.Maui.Views;
using UangKu.Model.Base;
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

        _vm.LoadData(ParameterModel.ItemDefaultValue.FirstPage, ParameterModel.ItemDefaultValue.Maxresult);
    }

    private void Btn_NextPage_Clicked(object sender, EventArgs e)
    {
		_vm.NextPage_Clicked(ParameterModel.ItemDefaultValue.Maxresult);
    }

    private void Btn_PreviousPage_Clicked(object sender, EventArgs e)
    {
        _vm.PreviousPage_Click(ParameterModel.ItemDefaultValue.Maxresult);
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