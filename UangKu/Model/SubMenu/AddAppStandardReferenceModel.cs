using System.Collections.ObjectModel;
using UangKu.Model.Base;
using UangKu.Model.Response.AppStandardReference;
using static UangKu.Model.Response.AppStandardReferenceItem.AppStandardReferenceItem;

namespace UangKu.Model.SubMenu
{
    public class AddAppStandardReferenceModel : BaseModel
    {
        private bool isedit = false;
        public bool IsEdit { get => isedit; set => SetProperty(ref isedit, value); }
        private IList<AppStandardReferenceID.Datum> listasr { get; set; }

        public IList<AppStandardReferenceID.Datum> ListASR
        {
            get
            {
                if (listasr == null)
                {
                    listasr = new ObservableCollection<AppStandardReferenceID.Datum>();
                }
                return listasr;
            }
            set { listasr = value; }
        }
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
    }
}
