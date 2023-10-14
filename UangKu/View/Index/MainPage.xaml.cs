using UangKu.ViewModel.Index;

namespace UangKu.View.Index;

public partial class MainPage : ContentPage
{
	private readonly MainPageVM _vm;
	public MainPage()
	{
		InitializeComponent();
		_vm = new MainPageVM();
		BindingContext = _vm;
	}
    protected override void OnAppearing()
    {
        _vm.LoadData(Ent_Username, Ent_Password, Btn_Login);
    }

    private void Btn_Login_Clicked(object sender, EventArgs e)
    {
		_vm.BtnLogin_User(Ent_Username, Ent_Password);
    }
}