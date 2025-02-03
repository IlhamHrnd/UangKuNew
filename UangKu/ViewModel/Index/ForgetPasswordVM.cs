using UangKu.Model.Base;
using UangKu.Model.Index;

namespace UangKu.ViewModel.Index
{
    public class ForgetPasswordVM : ForgetPassword
    {
        public ForgetPasswordVM()
        {
            Title = "Forget Password";
        }

        public async void BtnUpdate_User(Entry username, Entry email, Entry password, Entry confirmpass)
        {
            IsBusy = true;
            try
            {
                bool isValidEntry = await ValidateNullChecker.EntryValidateFields(
                    (username.Text, "Username"),
                    (password.Text, "Password"),
                    (confirmpass.Text, "Confirm Password"),
                    (email.Text, "Email")
                );

                if (!Network.IsConnected)
                {
                    await MsgModel.MsgNotification(ItemManager.Offline);
                    return;
                }
                if (!Equals(password.Text, confirmpass.Text))
                {
                    await MsgModel.MsgNotification($"Password And Confirm Password Are Not The Same");
                    return;
                }
                if (isValidEntry)
                {
                    var filter = new WebService.Filter.Root<WebService.Filter.User>
                    {
                        Data = new WebService.Filter.User
                        {
                            Username = username.Text,
                            Email = email.Text,
                            Password = password.Text
                        }
                    };
                    var update = await WebService.Service.User.UpdatePasswordUser(filter);
                    await MsgModel.MsgNotification(update.Message);
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
