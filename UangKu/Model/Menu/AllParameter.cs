using System.Collections.ObjectModel;
using UangKu.Model.Base;
using static UangKu.Model.Response.AppParameter.GetAllAppParameter;

namespace UangKu.Model.Menu
{
    public class AllParameter : BaseModel
    {
        private string title = string.Empty;
        public string Title { get => title; set => SetProperty(ref title, value); }
        private bool isbusy = false;
        public bool IsBusy { get => isbusy; set => SetProperty(ref isbusy, value); }
        private int page = 0;
        public int Page { get => page; set => page = value; }
        private int totalrecords = 0;
        public int TotalRecords { get => totalrecords; set => totalrecords = value; }
        private IList<GetAllAppParameterRoot> listparameter { get; set; }

        public IList<GetAllAppParameterRoot> ListParameter
        {
            get
            {
                if (listparameter == null)
                {
                    listparameter = new ObservableCollection<GetAllAppParameterRoot>();
                }
                return listparameter;
            }
            set { listparameter = value; }
        }
    }
}
