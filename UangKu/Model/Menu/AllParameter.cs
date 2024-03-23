using System.Collections.ObjectModel;
using UangKu.Model.Base;
using static UangKu.Model.Response.AppParameter.GetAllAppParameter;

namespace UangKu.Model.Menu
{
    public class AllParameter : BaseModel
    {
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
