using System.Collections.ObjectModel;
using UangKu.Model.Base;
using static UangKu.Model.Response.AppStandardReferenceItem.AppStandardReferenceItem;

namespace UangKu.Model.SubMenu
{
    public class WishlistEdit : BaseModel
    {
        private string title = string.Empty;
        public string Title { get => title; set => SetProperty(ref title, value); }
        private bool isbusy = false;
        public bool IsBusy { get => isbusy; set => SetProperty(ref isbusy, value); }
        private string mode = string.Empty;
        public string Mode { get => mode; set => SetProperty(ref mode, value); }
        private string wishlistid = string.Empty;
        public string WishlistID { get => wishlistid; set => SetProperty(ref wishlistid, value); }
        private bool isenabled = false;
        public bool IsEnabled { get => isenabled; set =>  SetProperty(ref isenabled, value); }
        private bool isenabled2 = false;
        public bool IsEnabled2 { get => isenabled2; set => SetProperty(ref isenabled2, value); }
        private bool isvisible = false;
        public bool IsVisible { get => isvisible; set => SetProperty(ref isvisible, value); }
        private bool isvisible2 = false;
        public bool IsVisible2 { get => isvisible2; set => SetProperty(ref isvisible2, value); }
        private string linkproduct = string.Empty;
        public string LinkProduct { get => linkproduct; set => SetProperty(ref linkproduct, value); }
        private IList<AsriRoot> listwishlist { get; set; }

        public IList<AsriRoot> ListWishList
        {
            get
            {
                if (listwishlist == null)
                {
                    listwishlist = new ObservableCollection<AsriRoot>();
                }
                return listwishlist;
            }
            set { listwishlist = value; }
        }
        private AsriRoot selectedwishlistid { get; set; }
        public AsriRoot SelectedWishlistID
        {
            get { return selectedwishlistid; }
            set
            {
                if (selectedwishlistid != value)
                {
                    selectedwishlistid = value;
                    OnPropertyChanged(nameof(SelectedWishlistID));
                }
            }
        }
    }
}
