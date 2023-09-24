namespace UangKu.Model.Base
{
    public class NetworkModel
    {
        private static readonly NetworkModel network = new NetworkModel();
        public bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        public NetworkModel()
        {
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        private async void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (!IsConnected)
            {
                await MsgModel.MsgNotification("You're Offline");
            }
            else
            {
                await MsgModel.MsgNotification("Back Online");
            }
        }
        public static NetworkModel Instance => network;
    }
}
