using System.Collections.ObjectModel;
using UangKu.Model.Base;
using UangKu.WebService.Data;

namespace UangKu.Model.Menu
{
    public class HomePage : BaseModel
    {
        private Root<ObservableCollection<WebService.Data.Transaction.Data>> summary;
        public Root<ObservableCollection<WebService.Data.Transaction.Data>> Summary
        {
            get
            {
                if (summary == null)
                {
                    summary = new Root<ObservableCollection<WebService.Data.Transaction.Data>>
                    {
                        Data = new ObservableCollection<WebService.Data.Transaction.Data>(),
                        Succeeded = true,
                        Errors = null,
                        Message = "Initialized"
                    };
                }
                return summary;
            }
            set
            {
                summary = value;
                OnPropertyChanged(nameof(Summary));
            }
        }

        private Root<ObservableCollection<WebService.Data.Transaction.Data>> transaction;
        public Root<ObservableCollection<WebService.Data.Transaction.Data>> Transaction
        {
            get
            {
                if (transaction == null)
                {
                    transaction = new Root<ObservableCollection<WebService.Data.Transaction.Data>>
                    {
                        Data = new ObservableCollection<WebService.Data.Transaction.Data>(),
                        Succeeded = true,
                        Errors = null,
                        Message = "Initialized"
                    };
                }
                return transaction;
            }
            set
            {
                transaction = value;
                OnPropertyChanged(nameof(Transaction));
            }
        }

        private Root<ObservableCollection<UserPicture.Data>> gallery;
        public Root<ObservableCollection<UserPicture.Data>> Gallery
        {
            get
            {
                if (gallery == null)
                {
                    gallery = new Root<ObservableCollection<UserPicture.Data>>
                    {
                        Data = new ObservableCollection<UserPicture.Data>(),
                        Succeeded = true,
                        Errors = null,
                        Message = "Initialized"
                    };
                }
                return gallery;
            }
            set
            {
                gallery = value;
                OnPropertyChanged(nameof(Gallery));
            }
        }
    }
}
