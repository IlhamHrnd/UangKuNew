using UangKu.Model.Base;
using UangKu.ViewModel.Menu;

namespace UangKu.View.Menu;

public partial class Transaction : ContentPage
{
	private readonly TransactionVM _vm;
	public Transaction()
	{
		InitializeComponent();
		_vm = new TransactionVM();
		BindingContext = _vm;
	}

    protected async override void OnAppearing()
    {
        await SessionModel.SessionCheck();
        _vm.LoadData();
    }

    private void RadioFilter_SelectedItemChanged(object sender, EventArgs e)
    {
        _vm.RadioSelection(RadioFilter.SelectedIndex);
    }
}