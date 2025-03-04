using System.ComponentModel;
using System.Runtime.CompilerServices;
using UangKu.Interface.Base;
using UangKu.Model.Session;

namespace UangKu.Model.Base
{
    public class BaseModel : INotifyPropertyChanged, IBaseModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // 🔒 Private Variables (Top)
        private string title = string.Empty;
        private bool isbusy = false;
        private int pagenumber = 0;
        private int pagesize = 0;
        private int totalrecords = 0;
        private int totalpages = 0;
        private NetworkModel network = NetworkModel.Instance;
        private string mode = string.Empty;
        private bool isProgram = false;
        private bool isProgramAddAble = false;
        private bool isProgramEditAble = false;
        private bool isProgramDeleteAble = false;
        private bool isProgramViewAble = false;
        private bool isProgramApprovalAble = false;
        private bool isProgramUnApprovalAble = false;
        private bool isProgramVoidAble = false;
        private bool isProgramUnVoidAble = false;
        private bool isVisible = false;
        private bool isUsedBySystem = false;
        private static int timeout;
        private static string url;
        private string userid = string.Empty;
        private DateTime startDate = new(DateTime.Now.Year, DateTime.Now.Month, 1);
        private DateTime endDate = new(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        private string prevPageLink;
        private string nextPageLink;
        private INavigation navigation;
        private bool isadded;
        private bool isedited;
        private bool isenabled;

        // 🌐 Public Properties (Bottom)
        public string Title { get => title; set => SetProperty(ref title, value); }
        public bool IsBusy { get => isbusy; set => SetProperty(ref isbusy, value); }
        public int PageNumber { get => pagenumber; set => SetProperty(ref pagenumber, value); }
        public int PageSize { get => pagesize; set => SetProperty(ref pagesize, value); }
        public int TotalRecords { get => totalrecords; set => SetProperty(ref totalrecords, value); }
        public int TotalPages { get => totalpages; set => SetProperty(ref totalpages, value); }
        public NetworkModel Network { get => network; set => network = value; }
        public string Mode { get => mode; set => SetProperty(ref mode, value); }
        public bool IsProgram { get => isProgram; set => SetProperty(ref isProgram, value); }
        public bool IsProgramAddAble { get => isProgramAddAble; set => SetProperty(ref isProgramAddAble, value); }
        public bool IsProgramEditAble { get => isProgramEditAble; set => SetProperty(ref isProgramEditAble, value); }
        public bool IsProgramDeleteAble { get => isProgramDeleteAble; set => SetProperty(ref isProgramDeleteAble, value); }
        public bool IsProgramViewAble { get => isProgramViewAble; set => SetProperty(ref isProgramViewAble, value); }
        public bool IsProgramApprovalAble { get => isProgramApprovalAble; set => SetProperty(ref isProgramApprovalAble, value); }
        public bool IsProgramUnApprovalAble { get => isProgramUnApprovalAble; set => SetProperty(ref isProgramUnApprovalAble, value); }
        public bool IsProgramVoidAble { get => isProgramVoidAble; set => SetProperty(ref isProgramVoidAble, value); }
        public bool IsProgramUnVoidAble { get => isProgramUnVoidAble; set => SetProperty(ref isProgramUnVoidAble, value); }
        public bool IsVisible { get => isVisible; set => SetProperty(ref isVisible, value); }
        public bool IsUsedBySystem { get => isUsedBySystem; set => SetProperty(ref isUsedBySystem, value); }
        public static int TimeOut
        {
            get => timeout = AppParameter.Timeout > 0 ? AppParameter.Timeout : ItemManager.Timeout;
            set => timeout = value > 0 ? value : ItemManager.Timeout;
        }
        public static string URL
        {
            get => url = !string.IsNullOrEmpty(AppParameter.URL) ? AppParameter.URL : ItemManager.Url;
            set => url = !string.IsNullOrEmpty(value) ? value : ItemManager.Url;
        }
        public string UserID
        {
            get
            {
                if (!string.IsNullOrEmpty(App.Session.personID))
                    userid = App.Session.personID;
                else if (string.IsNullOrEmpty(App.Session.personID) && !string.IsNullOrEmpty(App.Session.username))
                    userid = App.Session.username;
                else
                    userid = string.Empty;
                return userid;
            }
            set { userid = !string.IsNullOrEmpty(value) ? value : string.Empty; }
        }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }
        public string PrevPageLink { get => prevPageLink; set => SetProperty(ref prevPageLink, value); }
        public string NextPageLink { get => nextPageLink; set => SetProperty(ref nextPageLink, value); }
        public INavigation Navigation { get => navigation; set => navigation = value; }
        public bool IsAdded
        {
            get
            {
                if (Mode == ItemManager.NewFile)
                    return true;
                if (IsProgramAddAble)
                    return true;
                return false;
            }
            set { SetProperty(ref isadded, value); }
        }
        public bool IsEdited
        {
            get
            {
                if (Mode == ItemManager.EditFile)
                    return true;
                if (IsProgramEditAble)
                    return true;
                return false;
            }
            set { SetProperty(ref isedited, value); }
        }
        public bool IsEnabled
        {
            get
            {
                if (Mode == ItemManager.NewFile)
                    return IsAdded;
                else if (Mode == ItemManager.EditFile)
                    return IsEdited;
                else
                    return false;
            }
            set { SetProperty(ref isenabled, value); }
        }
        #region Load Function
        public async void AppProgram(string programID)
        {
            IsBusy = true;
            try
            {
                var program = await WebService.Service.AppProgram.GetAppProgramID(new WebService.Filter.Root<WebService.Filter.AppProgram>
                {
                    Data = new WebService.Filter.AppProgram
                    {
                        ProgramID = programID
                    }
                });
                if (program.Succeeded == true)
                {
                    Title = program.Data.programName;
                    IsProgram = program.Data.isProgram;
                    IsProgramAddAble = program.Data.isProgramAddAble ?? false;
                    IsProgramEditAble = program.Data.isProgramEditAble ?? false;
                    IsProgramDeleteAble = program.Data.isProgramDeleteAble ?? false;
                    IsProgramViewAble = program.Data.isProgramViewAble ?? false;
                    IsProgramApprovalAble = program.Data.isProgramApprovalAble ?? false;
                    IsProgramUnApprovalAble = program.Data.isProgramUnApprovalAble ?? false;
                    IsProgramVoidAble = program.Data.isProgramVoidAble ?? false;
                    IsProgramUnVoidAble = program.Data.isProgramUnVoidAble ?? false;
                    IsVisible = program.Data.isVisible ?? false;
                    IsUsedBySystem = program.Data.isUsedBySystem ?? false;
                }
                else
                    await MsgModel.MsgNotification(program.Message);
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

        public bool OnGetAddStatus()
        {
            return Mode == ItemManager.NewFile || isProgramAddAble;
        }

        public bool OnGetEditStatus()
        {
            return Mode == ItemManager.EditFile || isProgramEditAble;
        }
        #endregion

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
                return false;

            backingField = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
