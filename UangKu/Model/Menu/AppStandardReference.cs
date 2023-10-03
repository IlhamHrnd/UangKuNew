using System.Collections.ObjectModel;
using UangKu.Model.Base;
using static UangKu.Model.Response.AppStandardReference.AppStandardReference;

namespace UangKu.Model.Menu
{
    public class AppStandardReference : BaseModel
    {
        public AppStandardReference()
        {
            
        }
        private string title = "App Standard Reference";
        public string Title { get => title; set => SetProperty(ref title, value); }
        private string id = string.Empty;
        public string ID { get => id; set => SetProperty(ref id, value); }
        private bool isbusy = false;
        public bool IsBusy { get => isbusy; set => SetProperty(ref isbusy, value); }
        private int page = 0;
        public int Page { get => page; set => page = value; }
        private IList<AppStandardReferenceRoot> listasr { get; set; }

        public IList<AppStandardReferenceRoot> ListASR
        {
            get
            {
                if (listasr == null)
                {
                    listasr = new ObservableCollection<AppStandardReferenceRoot>();
                }
                return listasr;
            }
            set { listasr = value; }
        }
    }
}
