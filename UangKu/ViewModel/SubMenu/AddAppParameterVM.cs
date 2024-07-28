using UangKu.Model.Base;
using UangKu.Model.SubMenu;
using UangKu.ViewModel.RestAPI.AppParameter;
using static UangKu.Model.Response.AppStandardReferenceItem.AppStandardReferenceItem;

namespace UangKu.ViewModel.SubMenu
{
    public class AddAppParameterVM : AddAppParameter
    {
        private NetworkModel network = NetworkModel.Instance;
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

        public async void LoadData(Entry Ent_ParameterID, Entry Ent_ParameterNote, Entry Ent_ParameterValue, CheckBox CB_ParameterIsActive, Picker Pic_ParameterType,
            CheckBox CB_ParameterValue)
        {
            bool isConnect = network.IsConnected;
            IsBusy = true;
            try
            {
                if (!isConnect)
                {
                    await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                }
                var param = await RestAPI.AppStandardReferenceItem.AppStandardReferenceItem.GetAsriAsync<AsriRoot>("Control", true, true);
                if (param.Count > 0)
                {
                    ListParameterType.Clear();
                    for (int i = 0; i < param.Count; i++)
                    {
                        ListParameterType.Add(param[i]);
                    }
                }
                if (Mode == ParameterModel.ItemDefaultValue.NewFile)
                {

                }
                else if (Mode == ParameterModel.ItemDefaultValue.EditFile)
                {
                    if (!string.IsNullOrEmpty(ParameterID))
                    {
                        var parameterID = await GetParameterID.GetParameter(ParameterID);
                        if (parameterID.metaData.isSucces && parameterID.metaData.code == 200)
                        {
                            Ent_ParameterID.Text = parameterID.data.parameterID;
                            Ent_ParameterNote.Text = parameterID.data.parameterName;
                            Ent_ParameterValue.Text = parameterID.data.parameterValue;
                            CB_ParameterIsActive.IsChecked = (bool)parameterID.data.isUsedBySystem;
                            switch (parameterID.data.srControl)
                            {
                                case "Control-001":
                                    IsCheckedBoxVisible = true;
                                    IsEntryVisible = false;
                                    CB_ParameterValue.IsChecked = Converter.StringToBool(parameterID.data.parameterValue, false);
                                    break;

                                case "Control-002":
                                    IsEntryVisible = true;
                                    IsCheckedBoxVisible = false;
                                    Ent_ParameterValue.Text = parameterID.data.parameterValue;
                                    break;
                            }
                            var newParamList = Converter.ConvertIListToList(ListParameterType);
                            int selectedIndex = ControlHelper.GetIndexByName(newParamList, item => item.itemID, parameterID.data.srControl);
                            Pic_ParameterType.SelectedIndex = selectedIndex;
                            Pic_ParameterType.IsEnabled = string.IsNullOrEmpty(parameterID.data.srControl);
                        }
                        else
                        {
                            await MsgModel.MsgNotification(parameterID.metaData.message);
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

        public async Task SaveAppParameter_Click(Entry Ent_ParameterID, Entry Ent_ParameterNote, Entry Ent_ParameterValue, CheckBox CB_ParameterIsActive, Picker Pic_ParameterType,
            CheckBox CB_ParameterValue)
        {
            bool isConnect = network.IsConnected;
            bool isValidEntry;
            IsBusy = true;
            var sessionID = App.Session;
            string userID = SessionModel.GetUserID(sessionID);
            string parameterValue = string.Empty;
            try
            {
                switch (ParameterTypes)
                {
                    case "Control-001":
                        isValidEntry = await ValidateNullChecker.EntryValidateFields(
                                (Ent_ParameterID.Text, "ParameterID"),
                                (Ent_ParameterNote.Text, "Parameter Note")
                            );
                        break;
                    case "Control-002":
                        isValidEntry = await ValidateNullChecker.EntryValidateFields(
                                (Ent_ParameterID.Text, "ParameterID"),
                                (Ent_ParameterNote.Text, "Parameter Note"),
                                (Ent_ParameterValue.Text, "Parameter Value")
                            );
                        break;
                    default:
                        isValidEntry = false;
                        break;
                }

                if (!isConnect)
                {
                    await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                }
                if (string.IsNullOrEmpty(ParameterTypes))
                {
                    await MsgModel.MsgNotification($"Select Parameter Type First");
                }
                else
                {
                    switch (ParameterTypes)
                    {
                        case "Control-001":
                            parameterValue = CB_ParameterValue.IsChecked.ToString();
                            break;

                        case "Control-002":
                            parameterValue = Ent_ParameterValue.Text;
                            break;

                        default:
                            parameterValue = string.Empty;
                            break;
                    }
                }
                if (Mode == ParameterModel.ItemDefaultValue.NewFile && !string.IsNullOrEmpty(parameterValue) && isValidEntry)
                {
                    var bodyPatch = new Model.Index.Body.PostAppParameter
                    {
                        parameterID = Ent_ParameterID.Text,
                        parameterName = Ent_ParameterNote.Text,
                        parameterValue = parameterValue,
                        isUsedBySystem = CB_ParameterIsActive.IsChecked,
                        lastUpdateDateTime = ParameterModel.DateFormat.DateTime,
                        lastUpdateByUserID = userID,
                        srControl = SelectedParameterType.itemID
                    };

                    var parameter = await PostAppParameter.PostAppParameterID(bodyPatch);
                    if (!string.IsNullOrEmpty(parameter))
                    {
                        await MsgModel.MsgNotification($"{parameter}");
                    }
                }
                else if (Mode == ParameterModel.ItemDefaultValue.EditFile && !string.IsNullOrEmpty(parameterValue) && isValidEntry)
                {
                    var bodyPatch = new Model.Index.Body.PatchParameter
                    {
                        parameterID = Ent_ParameterID.Text,
                        parameterName = Ent_ParameterNote.Text,
                        parameterValue = parameterValue,
                        isUsedBySystem = CB_ParameterIsActive.IsChecked,
                        lastUpdateDateTime = ParameterModel.DateFormat.DateTime,
                        lastUpdateByUserID = userID,
                        srControl = SelectedParameterType.itemID
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

        public void PickerParameterType_Changed(Picker PickerParameterType)
        {
            if (PickerParameterType.SelectedItem != null)
            {
                var paramType = SelectedParameterType.itemID.ToString();
                switch (paramType)
                {
                    case "Control-001":
                        IsCheckedBoxVisible = true;
                        IsEntryVisible = false;
                        ParameterTypes = paramType;
                        break;

                    case "Control-002":
                        IsEntryVisible = true;
                        IsCheckedBoxVisible = false;
                        ParameterTypes = paramType;
                        break;

                    default:
                        ParameterTypes = string.Empty;
                        break;
                }
            }
        }
    }
}
