using CommunityToolkit.Maui.Views;
using UangKu.ViewModel.Menu;

namespace UangKu.View.Menu;

public partial class AppStandardReferenceItem : Popup
{
	private readonly AppStandardReferenceItemVM _vm;
	public AppStandardReferenceItem(string id)
	{
		InitializeComponent();
		_vm = new AppStandardReferenceItemVM(id);
		BindingContext = _vm;
	}
}