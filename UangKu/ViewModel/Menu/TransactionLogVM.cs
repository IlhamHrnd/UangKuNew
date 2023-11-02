using UangKu.Model.Menu;

namespace UangKu.ViewModel.Menu
{
    public class TransactionLogVM : TransactionLog
    {
        public TransactionLogVM()
        {
            Title = $"Transaction Log For {App.Session.username}";
        }
    }
}
