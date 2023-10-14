using System.Collections.ObjectModel;
using UangKu.Model.Base;
using static UangKu.Model.Response.AppStandardReference.AppStandardReferenceID;

namespace UangKu.Model.SubMenu
{
    public class AddAppStandardReferenceModel : BaseModel
    {
        private string title = "Add New Standard Reference";
        public string Title { get => title; set => SetProperty(ref title, value); }
        private bool isbusy = false;
        public bool IsBusy { get => isbusy; set => SetProperty(ref isbusy, value); }
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
