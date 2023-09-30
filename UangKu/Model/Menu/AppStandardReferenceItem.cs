using System.Collections.ObjectModel;
using UangKu.Model.Base;
using static UangKu.Model.Response.AppStandardReference.AppStandardReferenceID;
using static UangKu.Model.Response.AppStandardReferenceItem.AppStandardReferenceItem;

namespace UangKu.Model.Menu
{
    public class AppStandardReferenceItem : BaseModel
    {
        public AppStandardReferenceItem()
        {
            
        }
        private string title = string.Empty;
        public string Title { get => title; set => SetProperty(ref title, value); }
        private bool isbusy = false;
        public bool IsBusy { get => isbusy; set => SetProperty(ref isbusy, value); }
        private IList<AsriRoot> listasri { get; set; }

        public IList<AsriRoot> ListASRI
        {
            get
            {
                if (listasri == null)
                {
                    listasri = new ObservableCollection<AsriRoot>();
                }
                return listasri;
            }
            set { listasri = value; }
        }
        private IList<AppStandardReferenceIDRoot> listasr { get; set; }

        public IList<AppStandardReferenceIDRoot> ListASR
        {
            get
            {
                if (listasr == null)
                {
                    listasr = new ObservableCollection<AppStandardReferenceIDRoot>();
                }
                return listasr;
            }
            set { listasr = value; }
        }
    }
}
