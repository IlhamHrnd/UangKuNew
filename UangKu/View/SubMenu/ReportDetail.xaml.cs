using UangKu.Model.Base;
using UangKu.ViewModel.SubMenu;

namespace UangKu.View.SubMenu;

public partial class ReportDetail : ContentPage
{
	private readonly ReportDetailVM _vm;
	public ReportDetail(string mode)
	{
		InitializeComponent();
		_vm = new ReportDetailVM(Navigation, mode);
		BindingContext = _vm;
	}

    protected async override void OnAppearing()
    {
        await SessionModel.SessionCheck();
        _vm.SetVisibility(Btn_NewReport);
		_vm.LoadData();
    }

    private async void Btn_Upload_Clicked(object sender, EventArgs e)
    {
        await _vm.UploadPhoto_Click(Avt_ReportPhoto);
    }

    private async void Btn_NewReport_Clicked(object sender, EventArgs e)
    {
        await _vm.SaveReport_Click(Edt_Cronologic, Pic_ErrorLocation, Pic_ErrorPosibility, Dt_ErrorDate);
    }
}