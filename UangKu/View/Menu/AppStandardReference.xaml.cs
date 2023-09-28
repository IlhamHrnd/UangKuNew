using UangKu.ViewModel.Menu;

namespace UangKu.View.Menu;

public partial class AppStandardReference : ContentPage
{
	private readonly AppStandardReferenceVM _vm;
	public AppStandardReference()
	{
		InitializeComponent();
		_vm = new AppStandardReferenceVM();
		BindingContext = _vm;
	}
    protected override void OnAppearing()
    {
        _vm.LoadData();
    }
}