using UangKu.Model.Base;
using UangKu.Model.Master;

namespace UangKu.ViewModel.MasterPage
{
    public class MasterVM : Master
    {
        public MasterVM()
        {

        }

        public void BtnLogOut_Clicked()
        {
            App.Session = null;

            SessionCheck();
        }
        private async void SessionCheck()
        {
            if (App.Session == null)
            {
                await MsgModel.MsgNotification("You're Session Is Expired");

                var shell = new AppShell();

                App.Current.MainPage = shell;
            }
        }
    }
}
