using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace UangKu.Model.Base
{
    public static class MsgModel
    {
        public static async Task MsgNotification(string message)
        {
            var toast = Toast.Make(message, ToastDuration.Long);
            await toast.Show();
        }
    }
}
