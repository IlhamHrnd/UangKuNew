using UangKu.Model.Base;
using UangKu.Model.Index;

namespace UangKu.ViewModel.Index
{
    public class SignUpVM : SignUp
    {
        public SignUpVM()
        {
            Title = "Sign Up";
        }

        public async void LoadData()
        {
            IsBusy = true;
            try
            {
                if (!Network.IsConnected)
                {
                    await MsgModel.MsgNotification(ItemManager.Offline);
                    return;
                }
                var sex = await WebService.Service.AppStandardReferenceItem.GetAllReferenceItemID(new WebService.Filter.Root<WebService.Filter.AppStandardReferenceItem>
                {
                    Data = new WebService.Filter.AppStandardReferenceItem
                    {
                        StandardReferenceID = "Sex",
                        IsUsedBySystem = true,
                        IsActive = true
                    }
                });
                if (sex.Succeeded == true && sex.Data.Count > 0)
                {
                    ListSex.Clear();
                    for (int i = 0; i < sex.Data.Count; i++)
                    {
                        WebService.Data.AppStandardReferenceItem.Data item = sex.Data[i];
                        ListSex.Add(item);
                    }
                }
                else
                    await MsgModel.MsgNotification(sex.Message);
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
            IsBusy = true;
            try
            {
                bool isValidEntry = await ValidateNullChecker.EntryValidateFields(
                    (username.Text, "Username"),
                    (password.Text, "Password"),
                    (confirmpass.Text, "Confirm Password"),
                    (email.Text, "Email")
                );

                bool isValidPicker = await ValidateNullChecker.PickerValidateFields(
                    (sex, "Gender")
                );

                if (!Network.IsConnected)
                {
                    await MsgModel.MsgNotification(ItemManager.Offline);
                    return;
                }
                if (!Equals(password.Text, confirmpass.Text))
                {
                    await MsgModel.MsgNotification($"{password.Text} And {confirmpass.Text} Are Not The Same");
                    return;
                }
                if (isValidEntry && isValidPicker)
                {
                    var signup = await WebService.Service.User.CreateUsername(new WebService.Data.User.Data
                    {
                        Username = username.Text,
                        Password = password.Text,
                        Srsex = SelectedSex.itemId,
                        Email = email.Text,
                        Sraccess = "Access-02",
                        ActiveDate = DateFormat.DateTime,
                        LastLogin = DateFormat.DateTime,
                        LastUpdateDateTime = DateFormat.DateTime,
                        Srstatus = ItemManager.Status,
                        PersonId = username.Text,
                        LastUpdateByUser = username.Text
                    });
                    await MsgModel.MsgNotification(signup.Message);
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
