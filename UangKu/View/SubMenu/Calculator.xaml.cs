using UangKu.Model.Base;
using UangKu.ViewModel.SubMenu;

namespace UangKu.View.SubMenu;

public partial class Calculator : ContentPage
{
	private readonly CalculatorVM _vm;
	public Calculator()
	{
		InitializeComponent();
		_vm = new CalculatorVM(Navigation);
		BindingContext = _vm;
	}
    protected async override void OnAppearing()
    {
        await SessionModel.SessionCheck();
    }

    private void BtnClear_Clicked(object sender, EventArgs e)
    {
        _vm.OnClear(Lbl_Result);
    }

    private void BtnNumber_Clicked(object sender, EventArgs e)
    {
        _vm.OnSelectNumber(sender, Lbl_Result);
    }

    private void BtnNegative_Clicked(object sender, EventArgs e)
    {
        _vm.OnNegative(Lbl_Result, Lbl_Current);
    }

    private void BtnOperator_Clicked(object sender, EventArgs e)
    {
        _vm.OnSelectOperator(sender, Lbl_Result);
    }

    private void BtnCalculate_Clicked(object sender, EventArgs e)
    {
        _vm.OnCalculate(Lbl_Result, Lbl_Current);
    }

    private void Bar_AddItem_Clicked(object sender, EventArgs e)
    {
        _vm.OnComplete();
    }
}