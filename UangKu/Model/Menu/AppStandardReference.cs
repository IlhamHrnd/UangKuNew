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
