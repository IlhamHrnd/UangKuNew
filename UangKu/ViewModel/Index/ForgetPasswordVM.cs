using UangKu.Model.Base;
using UangKu.Model.Index;
using UangKu.ViewModel.RestAPI.User;

namespace UangKu.ViewModel.Index
{
    public class ForgetPasswordVM : ForgetPassword
    {
        private NetworkModel network = NetworkModel.Instance;
        public ForgetPasswordVM()
        {
            
        }

        public async void BtnUpdate_User(Entry username, Entry email, Entry password, Entry confirmpass)
        {
            bool isConnect = network.IsConnected;
            IsBusy = true;
            try
            {
                if (!isConnect)
                {
                    await MsgModel.MsgNotification("You're Offline");
                }
                if (string.IsNullOrEmpty(username.Text) || string.IsNullOrEmpty(email.Text) || string.IsNullOrEmpty(password.Text) || string.IsNullOrEmpty(confirmpass.Text))
                {
                    await MsgModel.MsgNotification($"Username Or Email Is Required");
                }
                else if (!Equals(password.Text, confirmpass.Text))
                {
                    await MsgModel.MsgNotification($"Password And Confirm Password Are Not The Same");
                }
                else
                {
                    var updatepassword = await UserForgotPassword.PatchUserForgotPassword(username.Text, email.Text, password.Text);
                    if (updatepassword != null)
                    {
                        await MsgModel.MsgNotification(updatepassword);
                    }
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
