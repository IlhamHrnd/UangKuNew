using UangKu.Model.Base;
using UangKu.Model.Session;
using UangKu.ViewModel.Menu;

namespace UangKu.View.Menu;

public partial class TransactionLog : ContentPage
{
	private readonly TransactionLogVM _vm;
    private int FirstPage = 1;
    DatePicker DatePicker = null;
    Picker Picker = new Picker();
    InputKit.Shared.Controls.CheckBox CheckBox = new InputKit.Shared.Controls.CheckBox();
    public TransactionLog()
	{
        InitializeComponent();
		_vm = new TransactionLogVM(Navigation);
		BindingContext = _vm;
	}
    protected async override void OnAppearing()
    {
        await SessionModel.SessionCheck();
		_vm.LoadData(FirstPage, Converter.StringToInt(AppParameter.MaxResult, ParameterModel.AppParameterDefault.Maxresult), DatePicker, DatePicker, Picker, CheckBox);
    }

    private void Btn_NextPage_Clicked(object sender, EventArgs e)
    {
        _vm.NextPage_Clicked(Converter.StringToInt(AppParameter.MaxResult, ParameterModel.AppParameterDefault.Maxresult), Date_StartDate, Date_EndDate, Pic_OrderBy, CB_IsAscending);
    }

    private void Btn_PreviousPage_Clicked(object sender, EventArgs e)
    {
        _vm.PreviousPage_Click(Converter.StringToInt(AppParameter.MaxResult, ParameterModel.AppParameterDefault.Maxresult), Date_StartDate, Date_EndDate, Pic_OrderBy, CB_IsAscending);
    }

    private async void Bar_AddItem_Clicked(object sender, EventArgs e)
    {
        await _vm.NewTransaction_ToolBar(ParameterModel.ItemDefaultValue.NewFile);
    }

    private async void SwipeTrans_Invoked(object sender, EventArgs e)
    {
        await _vm.SwipeItem_Invoked(sender, ParameterModel.ItemDefaultValue.EditFile);
    }

    private void Btn_SearchTransaction_Clicked(object sender, EventArgs e)
    {
        _vm.LoadData(FirstPage, Converter.StringToInt(AppParameter.MaxResult, ParameterModel.AppParameterDefault.Maxresult), Date_StartDate, Date_EndDate, Pic_OrderBy, CB_IsAscending);
    }
}