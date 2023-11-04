using UangKu.Model.Base;
using UangKu.ViewModel.SubMenu;

namespace UangKu.View.SubMenu;

public partial class NewTransaction : ContentPage
{
	private readonly NewTransactionVM _vm;
	public NewTransaction(string mode)
	{
		InitializeComponent();
		_vm = new NewTransactionVM(mode);
		BindingContext = _vm;
	}
    protected async override void OnAppearing()
    {
        await SessionModel.SessionCheck();
		_vm.LoadData();
    }

    private async void Pic_TransType_SelectedIndexChanged(object sender, EventArgs e)
    {
        await _vm.PickerTransType_Changed(Pic_TransType, Ent_TransNo);
    }
}