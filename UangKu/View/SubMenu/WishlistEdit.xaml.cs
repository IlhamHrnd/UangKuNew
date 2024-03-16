using UangKu.Model.Base;
using UangKu.ViewModel.SubMenu;

namespace UangKu.View.SubMenu;

public partial class WishlistEdit : ContentPage
{
	private readonly WishlistEditVM _vm;
	public WishlistEdit(string mode, string wishlistID)
	{
		InitializeComponent();
		_vm = new WishlistEditVM(mode, wishlistID);
		BindingContext = _vm;
	}

    protected async override void OnAppearing()
	{
        await SessionModel.SessionCheck();
		await _vm.LoadData(Lbl_ProductName, Btn_WishlistAction, Avt_Avatar, Pic_ProductCategory, Ent_Quantity, Ent_Price, CB_IsComplete, Date_WishlistDate);
    }

    private async void Btn_LinkProduct_Clicked(object sender, EventArgs e)
    {
		await _vm.LinkProduct_Clicked();
    }
}