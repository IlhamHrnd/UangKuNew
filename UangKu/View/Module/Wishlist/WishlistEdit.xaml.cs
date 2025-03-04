using UangKu.Model.Base;
using UangKu.ViewModel.Module.Wishlist;

namespace UangKu.View.Module.Wishlist;

public partial class WishlistEdit : ContentPage
{
	private readonly WishlistEditVM _vm;
	public WishlistEdit(string mode, string transNo)
	{
		InitializeComponent();
		_vm = new WishlistEditVM(mode, transNo);
		BindingContext = _vm;
	}

    protected async override void OnAppearing()
    {
        await SessionModel.SessionCheck();
        _vm.LoadData();
    }
}