using UangKu.Model.Base;
using UangKu.ViewModel.Module.Transaction;

namespace UangKu.View.Module.Transaction;

public partial class Transaction : ContentPage
{
	private readonly TransactionVM _vm;
	public Transaction()
	{
		InitializeComponent();
		_vm = new TransactionVM(Navigation);
		BindingContext = _vm;
	}

    protected async override void OnAppearing()
    {
        await SessionModel.SessionCheck();
        _vm.LoadData();
    }

    private void RadioFilter_SelectedItemChanged(object sender, EventArgs e)
    {
        _vm.RadioSelection(RadioFilter.SelectedIndex);
    }

    private void ButtonFilter_Clicked(object sender, EventArgs e)
    {
        _vm.FilterTransaction(RadioFilter.SelectedIndex);
    }

    private void ButtonPrevious_Clicked(object sender, EventArgs e)
    {
        _vm.NextPreviousPage(false, RadioFilter.SelectedIndex);
    }

    private void ButtonNext_Clicked(object sender, EventArgs e)
    {
        _vm.NextPreviousPage(true, RadioFilter.SelectedIndex);
    }

    private void ImgBtn_ScrollTop_Clicked(object sender, EventArgs e)
    {
        _vm.ScrollTopBottom(true, ScrollView);
    }

    private void ImgBtn_ScrollBottom_Clicked(object sender, EventArgs e)
    {
        _vm.ScrollTopBottom(false, ScrollView);
    }

    private async void ImgBtn_ExportPDF_Clicked(object sender, EventArgs e)
    {
        await _vm.GenerateReport();
    }

    private void SwipeItemLeft_Invoked(object sender, EventArgs e)
    {
        _ = _vm.SwipeItem(sender, ItemManager.EditFile);
    }

    private void SwipeItemRight_Invoked(object sender, EventArgs e)
    {
        _ = _vm.SwipeItem(sender, ItemManager.DeleteFile);
    }

    private void Bar_AddItem_Clicked(object sender, EventArgs e)
    {
        _ = _vm.ToolbarBottom();
    }
}