using UangKu.Model.Base;

namespace UangKu.ViewModel.Menu
{
    public class AppStandardReferenceItemVM : Model.Menu.AppStandardReferenceItem
    {
        private string Id { get; }
        public AppStandardReferenceItemVM(string id)
        {
            Id = id;

            _ = MsgModel.MsgNotification($"Your ID Is {id}");
        }
    }
}
