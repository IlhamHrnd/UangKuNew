namespace UangKu.Model.Base
{
    public class NetworkModel
    {
        private static readonly NetworkModel network = new NetworkModel();
        public static NetworkModel Instance => network;
        public bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        public NetworkModel()
        {
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }
        //Kondisi Saat Koneksi Berubah
        private async void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (!IsConnected)
            {
                await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
            }
            else
            {
                await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Online);
            }
        }
    }

    public static class SessionModel
    {
        public static async Task<bool> SessionCheck()
        {
            if (App.Session == null)
            {
                await MsgModel.MsgNotification("Your session has expired");
                var shell = new AppShell();
                App.Current.MainPage = shell;
                return false;
            }
            return true;
        }
    }
}
