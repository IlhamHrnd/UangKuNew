using UangKu.Model.Base;
using UangKu.Model.SubMenu;
using UangKu.ViewModel.RestAPI.AppStandardReferenceItem;

namespace UangKu.ViewModel.SubMenu
{
    public class AddAppStandardReferenceVM : AddAppStandardReferenceModel
    {
        private NetworkModel network = NetworkModel.Instance;
        public AddAppStandardReferenceVM()
        {

        }

        public async void ReferenceID_Complete(Entry referenceID, Entry referenceName, Entry itemLength, Entry note)
        {
            bool isConnect = network.IsConnected;
            IsBusy = true;
            try
            {
                ListASR.Clear();
                if (!isConnect)
                {
                    await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                }
                var asrid = await GetAppStandardReferenceID.GetASR(referenceID.Text);
                if (asrid != null)
                {
                    ListASR.Add(asrid);
                }
                switch (ListASR.Count)
                {
                    case > 0:
                        {
                            var item = ListASR[0];

                            referenceName.Text = item.standardReferenceName;
                            referenceName.IsReadOnly = true;
                            itemLength.Text = item.itemLength.ToString();
                            itemLength.IsReadOnly = true;
                            note.Text = item.note;
                            note.IsReadOnly = true;
                            break;
                        }

                    default:
                        referenceName.Text = string.Empty;
                        referenceName.IsReadOnly = false;
                        itemLength.Text = string.Empty;
                        itemLength.IsReadOnly = false;
                        note.Text = string.Empty;
                        note.IsReadOnly = false;
                        break;
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

        public async void AddNewASR(Entry referenceID, Entry referenceName, Entry itemLength, Entry note)
        {
            bool isConnect = network.IsConnected;
            IsBusy = true;
            try
            {
                if (!isConnect)
                {
                    await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                }
                if (string.IsNullOrEmpty(referenceID.Text) || string.IsNullOrEmpty(referenceName.Text) ||
                    string.IsNullOrEmpty(itemLength.Text) || string.IsNullOrEmpty(note.Text))
                {
                    await MsgModel.MsgNotification($"All Data Are Required");
                }
                else
                {
                    var addASR = await PostAppStandardReference.PostASR(referenceID.Text, referenceName.Text, int.Parse(itemLength.Text), note.Text);
                    if (!string.IsNullOrEmpty(addASR))
                    {
                        await MsgModel.MsgNotification($"{addASR}");
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
