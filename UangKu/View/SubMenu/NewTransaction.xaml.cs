using UangKu.Model.Base;
using UangKu.ViewModel.SubMenu;

namespace UangKu.View.SubMenu;

public partial class NewTransaction : ContentPage
{
	private readonly NewTransactionVM _vm;
	public NewTransaction(string mode, string transNo)
	{
		InitializeComponent();
		_vm = new NewTransactionVM(mode, transNo);
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

    private async void Btn_Upload_Clicked(object sender, EventArgs e)
    {
        await _vm.UploadPhoto_Click(Avt_Transaction);
    }

    private async void Btn_SaveTransaction_Clicked(object sender, EventArgs e)
    {
        await _vm.SaveTransaction_Click(Ent_TransNo, Ent_Amount, Ent_Description, Pic_TransType, Pic_TransItem);
    }
}