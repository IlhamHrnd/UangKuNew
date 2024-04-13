using UangKu.Model.Base;
using UangKu.Model.Session;
using UangKu.ViewModel.Menu;

namespace UangKu.View.Menu;

public partial class Wishlist : ContentPage
{
	private readonly WishlistVM _vm;
    private int FirstPage = 1;
    public Wishlist()
	{
		InitializeComponent();
		_vm = new WishlistVM(Navigation);
		BindingContext = _vm;
	}

    protected async override void OnAppearing()
    {
        await SessionModel.SessionCheck();
		await _vm.LoadData(FirstPage, AppParameter.MaxResult);
    }

    private async void SwipeTrans_Edit(object sender, EventArgs e)
    {
        await _vm.SwipeItem_Invoked(sender, ParameterModel.ItemDefaultValue.EditFile);
    }

    private async void SwipeTrans_View(object sender, EventArgs e)
    {
        await _vm.SwipeItem_Invoked(sender, ParameterModel.ItemDefaultValue.ViewFile);
    }

    private async void Bar_AddItem_Clicked(object sender, EventArgs e)
    {
        await _vm.WishlistNew_ToolBar(ParameterModel.ItemDefaultValue.NewFile);
    }
}