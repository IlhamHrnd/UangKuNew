using CommunityToolkit.Maui.Views;
using UangKu.Model.Base;
using UangKu.ViewModel.Menu;

namespace UangKu.View.Menu;

public partial class AppStandardReference : ContentPage
{
	private readonly AppStandardReferenceVM _vm;
	private int FirstPage = 1;
	private int PageSize = 25;
	public AppStandardReference()
	{
		InitializeComponent();
		_vm = new AppStandardReferenceVM();
		BindingContext = _vm;
	}
    protected override void OnAppearing()
    {
        _vm.LoadData(FirstPage, PageSize);
    }

    private void Btn_NextPage_Clicked(object sender, EventArgs e)
    {
		_vm.NextPage_Clicked(PageSize);
    }

    private void Btn_PreviousPage_Clicked(object sender, EventArgs e)
    {
        _vm.PreviousPage_Click(PageSize);
    }

    private async void Coll_AppStandardReference_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        await _vm.AppStandardReferenceItem_PopUp(e);

        if (!string.IsNullOrEmpty(_vm.ID))
        {
            var id = _vm.ID;
            var asri = new View.Menu.AppStandardReferenceItem(id);

            await this.ShowPopupAsync(asri);
        }
        else
        {
            await MsgModel.MsgNotification($"You Haven't Selected An Item Yet");
        }
    }
}