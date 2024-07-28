using System.Collections.ObjectModel;
using UangKu.Model.Base;
using UangKu.Model.Response.AppStandardReference;
using static UangKu.Model.Response.AppStandardReference.AppStandardReferenceID;
using static UangKu.Model.Response.AppStandardReferenceItem.AppStandardReferenceItem;

namespace UangKu.Model.Menu
{
    public class AppStandardReferenceItem : BaseModel
    {
        public AppStandardReferenceItem()
        {
            
        }
        private string id = string.Empty;
        public string Id { get => id; set => SetProperty(ref id, value); }
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
        private IList<AsriTwoRoot> listasritwo { get; set; }
        public IList<AsriTwoRoot> ListASRITwo
        {
            get
            {
                if (listasritwo == null)
                {
                    listasritwo = new ObservableCollection<AsriTwoRoot>();
                }
                return listasritwo;
            }
            set { listasritwo = value; }
        }
        private IList<AsriThreeRoot> listdifferentasri { get; set; }
        public IList<AsriThreeRoot> ListDifferentASRI
        {
            get
            {
                if (listdifferentasri == null)
                {
                    listdifferentasri = new ObservableCollection<AsriThreeRoot>();
                }
                return listdifferentasri;
            }
            set { listdifferentasri = value; }
        }
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
    }
}
