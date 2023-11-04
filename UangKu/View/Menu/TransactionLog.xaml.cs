using UangKu.Model.Base;
using UangKu.ViewModel.Menu;

namespace UangKu.View.Menu;

public partial class TransactionLog : ContentPage
{
	private readonly TransactionLogVM _vm;
    private int FirstPage = 1;
    public TransactionLog()
	{
        InitializeComponent();
		_vm = new TransactionLogVM(Navigation);
		BindingContext = _vm;
	}
    protected async override void OnAppearing()
    {
        await SessionModel.SessionCheck();
		_vm.LoadData(FirstPage, ParameterModel.ItemDefaultValue.Maxresult);
    }

    private void Btn_NextPage_Clicked(object sender, EventArgs e)
    {
        _vm.NextPage_Clicked(ParameterModel.ItemDefaultValue.Maxresult);
    }

    private void Btn_PreviousPage_Clicked(object sender, EventArgs e)
    {
        _vm.PreviousPage_Click(ParameterModel.ItemDefaultValue.Maxresult);
    }

    private async void Bar_AddItem_Clicked(object sender, EventArgs e)
    {
        await _vm.NewTransaction_ToolBar(ParameterModel.ItemDefaultValue.NewFile);
    }
}