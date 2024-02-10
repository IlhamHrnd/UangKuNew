using UangKu.Model.Base;
using UangKu.ViewModel.SubMenu;

namespace UangKu.View.SubMenu;

public partial class AddAppParameter : ContentPage
{
	private readonly AddAppParameterVM _vm;

	public AddAppParameter(string mode, string parameterID)
	{
		InitializeComponent();
		_vm = new AddAppParameterVM(mode, parameterID);
		BindingContext = _vm;
	}

    protected async override void OnAppearing()
    {
        await SessionModel.SessionCheck();
        _vm.LoadData(Ent_ParameterID, Ent_ParameterNote, Ent_ParameterValue, CB_ParameterIsActive);
    }

    private async void Btn_SaveAppParameter_Clicked(object sender, EventArgs e)
    {
        await _vm.SaveAppParameter_Click(Ent_ParameterID, Ent_ParameterNote, Ent_ParameterValue, CB_ParameterIsActive);
    }
}