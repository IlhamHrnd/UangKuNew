using UangKu.Model.Base;
using UangKu.Model.Menu;

namespace UangKu.ViewModel.Menu
{
    public class AppStandardReferenceVM : AppStandardReference
    {
        private NetworkModel network = NetworkModel.Instance;
        public AppStandardReferenceVM()
        {
            
        }
        public async void LoadData()
        {
            bool isConnect = network.IsConnected;
            IsBusy = true;
            try
            {
                if (!isConnect)
                {
                    await MsgModel.MsgNotification("You're Offline");
                }
                var asr = await RestAPI.AppStandardReferenceItem.AppStandardReference.GetAllASR(1, 25);
                if (asr.data.Count > 0)
                {
                    ListASR.Clear();
                    ListASR.Add(asr);
                }
            }
            catch (Exception e)
            {
                await MsgModel.MsgNotification(e.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
