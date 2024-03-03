using UangKu.ViewModel.Menu;

namespace UangKu.View.Menu;
public partial class AppStandardReferenceItem : ContentPage
{
	private readonly AppStandardReferenceItemVM _vm;
	public AppStandardReferenceItem(string itemID)
	{
		InitializeComponent();
		_vm = new AppStandardReferenceItemVM(itemID);
		BindingContext = _vm;
	}

    private async void Btn_UpdateAppStandard_Clicked(object sender, EventArgs e)
    {
		await _vm.BtnUpdateAppStandard_Click();
    }
}