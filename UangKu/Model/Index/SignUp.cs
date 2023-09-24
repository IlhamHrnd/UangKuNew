using System.Collections.ObjectModel;
using UangKu.Model.Base;
using static UangKu.Model.Response.AppStandardReferenceItem.Sex;

namespace UangKu.Model.Index
{
    public class SignUp : BaseModel
    {
        public SignUp()
        {
            
        }

        private string title = "Sign Up";
        public string Title { get => title; set => SetProperty(ref title, value); }
        private bool isbusy = false;
        public bool IsBusy { get => isbusy; set => SetProperty(ref isbusy, value); }
        private SexRoot selectedsex { get; set; }
        public SexRoot SelectedSex
        {
            get { return selectedsex; }
            set
            {
                if (selectedsex != value)
                {
                    selectedsex = value;
                    OnPropertyChanged(nameof(SelectedSex));
                }
            }
        }
        private IList<SexRoot> listsex { get; set; }

        public IList<SexRoot> ListSex
        {
            get
            {
                if (listsex == null)
                {
                    listsex = new ObservableCollection<SexRoot>();
                }
                return listsex;
            }
            set { listsex = value; }
        }
    }
}
