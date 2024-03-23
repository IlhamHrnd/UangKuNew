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

    private void Ent_StandardID_TextChanged(object sender, TextChangedEventArgs e)
    {
        _vm.ReferenceID_TextChanged(e, Ent_StandardID, Ent_ReferenceName, Ent_ItemLength, Ent_Note);
    }

    private void Btn_AddASR_Clicked(object sender, EventArgs e)
    {
        _vm.AddNewASR(Ent_StandardID, Ent_ReferenceName, Ent_ItemLength, Ent_Note);
    }

    private async void Bar_AddItem_Clicked(object sender, EventArgs e)
    {
        await _vm.AddASRIItem_ToolBar(Ent_StandardID, Ent_ItemID);
    }

    private async void Btn_AddItem_Clicked(object sender, EventArgs e)
    {
        await _vm.AddItem_Click(Ent_StandardID, Ent_ItemID, Ent_ItemName, Ent_NoteASRI);
    }

    private async void Btn_ASRIIcon_Clicked(object sender, EventArgs e)
    {
        await _vm.UploadItemIcon_Click();
    }
}