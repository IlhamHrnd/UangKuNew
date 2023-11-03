using UangKu.Model.Base;
using UangKu.ViewModel.Menu;

namespace UangKu.View.Menu;

public partial class TransactionLog : ContentPage
{
	private readonly TransactionLogVM _vm;
	public TransactionLog()
	{
		InitializeComponent();
		_vm = new TransactionLogVM();
		BindingContext = _vm;
	}
    protected async override void OnAppearing()
    {
        await SessionModel.SessionCheck();
		_vm.LoadData();
    }
}