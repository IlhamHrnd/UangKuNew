using UangKu.Model.Base;
using UangKu.ViewModel.Menu;

namespace UangKu.View.Menu;

public partial class UserReport : ContentPage
{
	private readonly UserReportVM _vm;
	public UserReport()
	{
		InitializeComponent();
		_vm = new UserReportVM(Navigation);
		BindingContext = _vm;
	}

    protected async override void OnAppearing()
    {
        await SessionModel.SessionCheck();
		_vm.SetVisibility();
		_vm.LoadData();
    }

    private async void Coll_Report_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        await _vm.UserReport_PopUp(e, ParameterModel.ItemDefaultValue.EditFile);
    }

    private async void Bar_AddItem_Clicked(object sender, EventArgs e)
    {
		await _vm.AddNewReport_ToolBar(ParameterModel.ItemDefaultValue.NewFile);
    }
}