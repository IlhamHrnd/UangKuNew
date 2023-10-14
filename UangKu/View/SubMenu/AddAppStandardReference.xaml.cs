using UangKu.ViewModel.SubMenu;

namespace UangKu.View.SubMenu;

public partial class AddAppStandardReference : ContentPage
{
	private readonly AddAppStandardReferenceVM _vm;
	public AddAppStandardReference()
	{
		InitializeComponent();
		_vm = new AddAppStandardReferenceVM();
		BindingContext = _vm;
	}

    private void Ent_StandardID_Completed(object sender, EventArgs e)
    {
		_vm.ReferenceID_Complete(Ent_StandardID, Ent_ReferenceName, Ent_ItemLength, Ent_Note);
    }

    private void Btn_AddASR_Clicked(object sender, EventArgs e)
    {
		_vm.AddNewASR(Ent_StandardID, Ent_ReferenceName, Ent_ItemLength, Ent_Note);
    }
}