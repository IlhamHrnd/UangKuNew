using UangKu.Model.Base;
using UangKu.Model.Index;
using UangKu.ViewModel.RestAPI.AppStandardReferenceItem;
using UangKu.ViewModel.RestAPI.User;
using static UangKu.Model.Response.AppStandardReferenceItem.AppStandardReferenceItem;

namespace UangKu.ViewModel.Index
{
    public class SignUpVM : SignUp
    {
        private NetworkModel network = NetworkModel.Instance;
        public SignUpVM()
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
                    await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                }
                var sex = await AppStandardReferenceItem.GetAsriAsync<AsriRoot>("Sex", true, true);
                if (sex.Count > 0)
                {
                    ListSex.Clear();
                    for (int i = 0; i < sex.Count; i++)
                    {
                        ListSex.Add(sex[i]);
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

        public async void SignUp_User(Entry username, Entry password, Entry confirmpass, Entry email, Picker sex)
        {
            bool isConnect = network.IsConnected;
            IsBusy = true;
            try
            {
                if (!isConnect)
                {
                    await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                }
                if (string.IsNullOrEmpty(username.Text) || string.IsNullOrEmpty(password.Text) ||
                    string.IsNullOrEmpty(confirmpass.Text) || string.IsNullOrEmpty(email.Text) || sex.SelectedItem == null)
                {
                    await MsgModel.MsgNotification($"All Data Are Required");
                }
                else if (!Equals(password.Text, confirmpass.Text))
                {
                    await MsgModel.MsgNotification($"{password.Text} And {confirmpass.Text} Are Not The Same");
                }
                else
                {
                    var signup = await UserSignUp.PostUserSignUp(username.Text, password.Text, SelectedSex.itemID, email.Text);
                    if (signup != null)
                    {
                        await MsgModel.MsgNotification(signup);
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
