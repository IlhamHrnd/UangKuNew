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
}