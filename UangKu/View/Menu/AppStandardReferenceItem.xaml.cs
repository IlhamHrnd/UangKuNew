namespace UangKu.View.Menu;

public partial class AppStandardReferenceItem
{
	private readonly ViewModel.Menu.AppStandardReferenceItemVM _vm;
	public AppStandardReferenceItem(string id)
	{
		InitializeComponent();
		_vm = new ViewModel.Menu.AppStandardReferenceItemVM(id);
		BindingContext = _vm;
	}
    protected override void OnAppearing()
    {
        _vm.LoadData();
    }
}