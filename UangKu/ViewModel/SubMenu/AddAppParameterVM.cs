using UangKu.Model.Base;
using UangKu.Model.SubMenu;
using UangKu.ViewModel.RestAPI.AppParameter;

namespace UangKu.ViewModel.SubMenu
{
    public class AddAppParameterVM : AddAppParameter
    {
        private NetworkModel network = NetworkModel.Instance;
        private string Mode { get; set; }
        private string ParameterID { get; set; }

        public AddAppParameterVM(string mode, string parameterID)
        {
            Mode = mode;
            ParameterID = parameterID;

            LoadTitle();
        }

        private async void LoadTitle()
        {
            if (Mode == ParameterModel.ItemDefaultValue.NewFile)
            {
                Title = $"Add New Parameter";
                IsReadOnly = false;
            }
            else if (Mode == ParameterModel.ItemDefaultValue.EditFile)
            {
                Title = $"Edit Parameter {ParameterID}";
                IsReadOnly = true;
            }
            else
            {
                Title = string.Empty;
                await MsgModel.MsgNotification($"Mode For {Mode} Is Unknow");
            }
        }

        public async void LoadData(Entry Ent_ParameterID, Entry Ent_ParameterNote, Entry Ent_ParameterValue, CheckBox CB_ParameterIsActive)
        {
            bool isConnect = network.IsConnected;
            IsBusy = true;
            try
            {
                if (!isConnect)
                {
                    await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                }
                if (Mode == ParameterModel.ItemDefaultValue.NewFile)
                {

                }
                else if (Mode == ParameterModel.ItemDefaultValue.EditFile)
                {
                    if (!string.IsNullOrEmpty(ParameterID))
                    {
                        var parameterID = await GetParameterID.GetParameter(ParameterID);
                        if (!string.IsNullOrEmpty(parameterID.parameterName))
                        {
                            Ent_ParameterID.Text = parameterID.parameterID;
                            Ent_ParameterNote.Text = parameterID.parameterName;
                            Ent_ParameterValue.Text = parameterID.parameterValue;
                            CB_ParameterIsActive.IsChecked = (bool)parameterID.isUsedBySystem;
                        }
                    }
                }
                else
                {
                    await MsgModel.MsgNotification($"{Mode} Not Define");
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

        public async Task SaveAppParameter_Click(Entry Ent_ParameterID, Entry Ent_ParameterNote, Entry Ent_ParameterValue, CheckBox CB_ParameterIsActive)
        {
            bool isConnect = network.IsConnected;
            IsBusy = true;
            var sessionID = App.Session;
            string userID = SessionModel.GetUserID(sessionID);
            try
            {
                bool isValidEntry = await ValidateNullChecker.EntryValidateFields(
                    (Ent_ParameterID.Text, "ParameterID"),
                    (Ent_ParameterNote.Text, "Parameter Note"),
                    (Ent_ParameterValue.Text, "Parameter Value")
                );

                if (!isConnect)
                {
                    await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                }
                if (Mode == ParameterModel.ItemDefaultValue.NewFile)
                {
                    var bodyPatch = new Model.Index.Body.PostAppParameter
                    {
                        parameterID = Ent_ParameterID.Text,
                        parameterName = Ent_ParameterNote.Text,
                        parameterValue = Ent_ParameterValue.Text,
                        isUsedBySystem = CB_ParameterIsActive.IsChecked,
                        lastUpdateDateTime = ParameterModel.DateFormat.DateTime,
                        lastUpdateByUserID = userID
                    };

                    var parameter = await PostAppParameter.PostAppParameterID(bodyPatch);
                    if (!string.IsNullOrEmpty(parameter))
                    {
                        await MsgModel.MsgNotification($"{parameter}");
                    }
                }
                else if (Mode == ParameterModel.ItemDefaultValue.EditFile)
                {
                    var bodyPatch = new Model.Index.Body.PatchParameter
                    {
                        parameterID = Ent_ParameterID.Text,
                        parameterName = Ent_ParameterNote.Text,
                        parameterValue = Ent_ParameterValue.Text,
                        isUsedBySystem = CB_ParameterIsActive.IsChecked,
                        lastUpdateDateTime = ParameterModel.DateFormat.DateTime,
                        lastUpdateByUserID = userID
                    };

                    var parameter = await PatchAppParameter.PatchParameterID(bodyPatch);
                    if (!string.IsNullOrEmpty(parameter))
                    {
                        await MsgModel.MsgNotification($"{parameter}");
                    }
                }
                else
                {
                    await MsgModel.MsgNotification($"Mode For {Mode} Is Unknow");
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
