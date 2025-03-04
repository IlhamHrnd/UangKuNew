using UangKu.Model.Base;
using UangKu.ViewModel.Module.Transaction;

namespace UangKu.View.Module.Transaction;

public partial class TransactionEdit : ContentPage
{
	private readonly TransactionEditVM _vm;
	public TransactionEdit(string mode, string transNo)
	{
		InitializeComponent();
		_vm = new TransactionEditVM(mode, transNo, Navigation);
		BindingContext = _vm;
	}

    protected async override void OnAppearing()
    {
        await SessionModel.SessionCheck();
        _vm.LoadData();
    }

    private void PickerItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        _vm.LoadItem();
    }

    private async void ButtonUpload_Clicked(object sender, EventArgs e)
    {
        await _vm.ImageUpload(AvatarTransaction);
    }

    private async void ButtonSave_Clicked(object sender, EventArgs e)
    {
        await _vm.SaveTransaction(Entry_Amount.Text, Entry_Description.Text, Date_TransactionDate.Date);
    }
}