using CommunityToolkit.Maui.Views;
using UangKu.Model.Base;
using UangKu.Model.SubMenu;
using static UangKu.Model.Base.ParameterModel.PermissionManager;
using static UangKu.Model.Response.AppStandardReferenceItem.AppStandardReferenceItem;

namespace UangKu.ViewModel.SubMenu
{
    public class ReportDetailVM : ReportDetail
    {
        private NetworkModel network = NetworkModel.Instance;
        private readonly INavigation _navigation;
        private string Mode { get; set; }
        public ReportDetailVM(INavigation navigation, string mode)
        {
            _navigation = navigation;
            Mode = mode;
        }

        public void SetVisibility(Button btnAddNewReport)
        {
            IsEnabled = App.Access.IsAdmin;
            if (Mode == ParameterModel.ItemDefaultValue.NewFile)
            {
                Title = $"New Report";
                btnAddNewReport.Text = "Add New Report";
            }
            else if (Mode == ParameterModel.ItemDefaultValue.EditFile)
            {
                Title = $"Report Detail {ParameterModel.Report.ReportNo}";
                btnAddNewReport.Text = $"Update Report {ParameterModel.Report.ReportNo}";
            }
            else
            {
                Title = string.Empty;
                btnAddNewReport.Text = string.Empty;
            }
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
                if (Mode == ParameterModel.ItemDefaultValue.NewFile)
                {
                    
                }
                else if (Mode == ParameterModel.ItemDefaultValue.EditFile)
                {

                }
                var location = await RestAPI.AppStandardReferenceItem.AppStandardReferenceItem.GetAsriAsync<AsriRoot>(ParameterModel.AppStandardReferenceItem.ErrorLocation, 
                    true, true);
                if (location.Count > 0)
                {
                    ListErrorLocation.Clear();
                    for (int i = 0; i < location.Count; i++)
                    {
                        ListErrorLocation.Add(location[i]);
                    }
                }

                var posibility = await RestAPI.AppStandardReferenceItem.AppStandardReferenceItem.GetAsriAsync<AsriTwoRoot>(ParameterModel.AppStandardReferenceItem.ErrorPossibility,
                    true, true);
                if (posibility.Count > 0)
                {
                    ListErrorPosibility.Clear();
                    for (int i = 0; i < posibility.Count; i++)
                    {
                        ListErrorPosibility.Add(posibility[i]);
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

        public async Task UploadPhoto_Click(AvatarView avatar)
        {
            PermissionType type = PermissionType.StorageRead;
            await PermissionRequest.RequestPermission(type);

            ImageSource source = await ImageConvert.PickImageAsync();

            if (source != null)
            {
                avatar.ImageSource = source;
                avatar.Text = ParameterModel.ImageManager.ImageName;
            }
        }

        public async Task SaveReport_Click(Editor editor, Picker PicLocation, Picker PicPosibility, DatePicker DatePic)
        {
            bool isConnect = network.IsConnected;
            IsBusy = true;
            try
            {
                bool isValidPicker = await ValidateNullChecker.PickerValidateFields(
                    (PicLocation, "Location"),
                    (PicPosibility, "Posibility")
                );

                if (!isConnect)
                {
                    await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                }
                if (isValidPicker)
                {
                    if (Mode == ParameterModel.ItemDefaultValue.NewFile)
                    {
                        var userID = SessionModel.GetUserID(App.Session);
                        var generateReportNo = SessionModel.GenerateUserReportNo(userID, App.Session.accessName);
                        var reportNo = await RestAPI.Report.NewReportNo.GetNewReportNo(generateReportNo);

                        if (string.IsNullOrEmpty(reportNo))
                        {
                            await MsgModel.MsgNotification($"Failed Generate New Report No");
                        }
                        else
                        {
                            var bodyPost = new Model.Index.Body.PostReport
                            {
                                reportNo = reportNo,
                                dateErrorOccured = DatePic.Date,
                                srErrorLocation = SelectedErrorLocation.itemID,
                                srErrorPossibility = SelectedErrorPosibility.itemID,
                                errorCronologic = editor.Text,
                                picture = ParameterModel.ImageManager.ImageString,
                                createdDateTime = ParameterModel.DateFormat.DateTime,
                                createdByUserID = userID,
                                lastUpdateDateTime = ParameterModel.DateFormat.DateTime,
                                lastUpdateByUserID = userID,
                                personID = userID
                            };

                            var report = await RestAPI.Report.PostReport.PostNewReport(bodyPost);
                            if (!string.IsNullOrEmpty(report))
                            {
                                await MsgModel.MsgNotification($"{report}");
                            }
                        }
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
