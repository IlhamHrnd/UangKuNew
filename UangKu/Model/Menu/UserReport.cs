using System.Collections.ObjectModel;
using UangKu.Model.Base;
using static UangKu.Model.Response.Report.Report;

namespace UangKu.Model.Menu
{
    public class UserReport : BaseModel
    {
        public UserReport()
        {
            
        }
        private string title = string.Empty;
        public string Title { get => title; set => SetProperty(ref title, value); }
        private bool isbusy = false;
        public bool IsBusy { get => isbusy; set => SetProperty(ref isbusy, value); }
        private bool isvisible = false;
        public bool IsVisible { get => isvisible; set => SetProperty(ref isvisible, value); }
        private int page = 0;
        public int Page { get => page; set => page = value; }
        private IList<WebService.Data.User.Data> listalluser { get; set; }

        public IList<WebService.Data.User.Data> ListAllUser
        {
            get
            {
                if (listalluser == null)
                {
                    listalluser = new ObservableCollection<WebService.Data.User.Data>();
                }
                return listalluser;
            }
            set { listalluser = value; }
        }
        private IList<ReportRoot> listreport { get; set; }
        public IList<ReportRoot> ListReport
        {
            get
            {
                if (listreport == null)
                {
                    listreport = new ObservableCollection<ReportRoot>();
                }
                return listreport;
            }
            set { listreport = value; }
        }
    }
}
