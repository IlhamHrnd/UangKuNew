using UangKu.Model.Base;
using UangKu.ViewModel.Module.Wishlist;

namespace UangKu.View.Module.Wishlist;

public partial class Wishlist : ContentPage
{
	private readonly WishlistVM _vm;
	public Wishlist()
	{
		InitializeComponent();
		_vm = new WishlistVM(Navigation);
		BindingContext = _vm;
	}

    protected async override void OnAppearing()
    {
        await SessionModel.SessionCheck();
        _vm.LoadData();
    }

    private void ImgBtn_ScrollTop_Clicked(object sender, EventArgs e)
    {
        _vm.ScrollTopBottom(true, ScrollView);
    }

    private void ImgBtn_ScrollBottom_Clicked(object sender, EventArgs e)
    {
        _vm.ScrollTopBottom(false, ScrollView);
    }

    private void SwipeItemRight_Invoked(object sender, EventArgs e)
    {
        _ = _vm.SwipeItem(sender, "right");
    }

    private void SwipeItemLeft_Invoked(object sender, EventArgs e)
    {
        _ = _vm.SwipeItem(sender, "left");
    }
}