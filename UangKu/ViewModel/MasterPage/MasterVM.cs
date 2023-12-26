using UangKu.Model.Base;
using UangKu.Model.Master;

namespace UangKu.ViewModel.MasterPage
{
    public class MasterVM : Master
    {
        public MasterVM()
        {

        }

        public async void BtnLogOut_Clicked()
        {
            App.Session = null;
            App.Access = null;
            App.Param = null;

            await SessionModel.SessionCheck();
        }
    }
}
