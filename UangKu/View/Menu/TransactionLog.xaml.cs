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
    InputKit.Shared.Controls.SelectionView SelectionView = new InputKit.Shared.Controls.SelectionView();
    public TransactionLog()
	{
        InitializeComponent();
		_vm = new TransactionLogVM(Navigation);
		BindingContext = _vm;
	}
    protected async override void OnAppearing()
    {
        await SessionModel.SessionCheck();
        _vm.LoadData(FirstPage, AppParameter.MaxResult, DatePicker, DatePicker, 
            Picker, CheckBox, CheckBox, SelectionView);
    }

    private void Btn_NextPage_Clicked(object sender, EventArgs e)
    {
        _vm.NextPreviousPage_Clicked(AppParameter.MaxResult, Date_StartDate, Date_EndDate, Pic_OrderBy, 
            CB_IsAscending, true, CB_FilterTransaction, SV_Filter);
    }

    private void Btn_PreviousPage_Clicked(object sender, EventArgs e)
    {
        _vm.NextPreviousPage_Clicked(AppParameter.MaxResult, Date_StartDate, Date_EndDate, Pic_OrderBy, 
            CB_IsAscending, false, CB_FilterTransaction, SV_Filter);
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
        _vm.LoadData(FirstPage, AppParameter.MaxResult, Date_StartDate, Date_EndDate, 
            Pic_OrderBy, CB_IsAscending, CB_FilterTransaction, SV_Filter);
    }

    private void ImgBtn_ScrollTop_Clicked(object sender, EventArgs e)
    {
        _vm.ScrollTopBottom_Clicked(ScrollView, 0, 0, true);
    }

    private void ImgBtn_ScrollBottom_Clicked(object sender, EventArgs e)
    {
        double y = ScrollView.ContentSize.Height;
        _vm.ScrollTopBottom_Clicked(ScrollView, 0, y, true);
    }

    private async void ImgBtn_ExportPDF_Clicked(object sender, EventArgs e)
    {
        await _vm.SavePDF();
    }
}