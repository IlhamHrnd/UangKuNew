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
            Title = "Forget Password";
        }

        public async void BtnUpdate_User(Entry username, Entry email, Entry password, Entry confirmpass)
        {
            bool isConnect = network.IsConnected;
            IsBusy = true;
            try
            {
                bool isValidEntry = await ValidateNullChecker.EntryValidateFields(
                    (username.Text, "Username"),
                    (password.Text, "Password"),
                    (confirmpass.Text, "Confirm Password"),
                    (email.Text, "Email")
                );

                if (!isConnect)
                {
                    await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                }
                if (!Equals(password.Text, confirmpass.Text))
                {
                    await MsgModel.MsgNotification($"Password And Confirm Password Are Not The Same");
                }
                if (isValidEntry)
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
