using System.Collections.ObjectModel;
using UangKu.Model.Base;
using UangKu.WebService.Data;

namespace UangKu.Model.Menu
{
    public class Transaction : BaseModel
    {
        private bool isallowcustomdate = false;
        private bool iscustomdaterange = false;
        private bool isascending = true;
        private DateTime firstdate = DateTime.Now;
        private DateTime lastdate = DateTime.Now;
        public bool IsAllowCustomDate { get => isallowcustomdate; set => SetProperty(ref isallowcustomdate, value); }
        public bool IsCustomDateRange { get => iscustomdaterange; set => SetProperty(ref iscustomdaterange, value); }
        public bool IsAscending { get => isascending; set => SetProperty(ref isascending, value); }
        public DateTime FirstDate 
        { 
            get => firstdate; 
            set 
            { 
                if (firstdate != value)
                {
                    firstdate = value;
                    OnPropertyChanged(nameof(FirstDate));
                }
            } 
        }
        public DateTime LastDate
        {
            get => lastdate;
            set
            {
                if (lastdate != value)
                {
                    lastdate = value;
                    OnPropertyChanged(nameof(LastDate));
                }
            }
        }

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

        private Root<ObservableCollection<WebService.Data.Transaction.Data>> trans;
        public Root<ObservableCollection<WebService.Data.Transaction.Data>> Trans
        {
            get
            {
                if (trans == null)
                {
                    trans = new Root<ObservableCollection<WebService.Data.Transaction.Data>>
                    {
                        Data = new ObservableCollection<WebService.Data.Transaction.Data>(),
                        Succeeded = true,
                        Errors = null,
                        Message = "Initialized"
                    };
                }
                return trans;
            }
            set
            {
                trans = value;
                OnPropertyChanged(nameof(Trans));
            }
        }

        private Root<ObservableCollection<WebService.Data.AppStandardReferenceItem.Data>> orderby;
        public Root<ObservableCollection<WebService.Data.AppStandardReferenceItem.Data>> OrderBy
        {
            get
            {
                if (orderby == null)
                {
                    orderby = new Root<ObservableCollection<AppStandardReferenceItem.Data>>
                    {
                        Data = new ObservableCollection<AppStandardReferenceItem.Data>(),
                        Succeeded = true,
                        Errors = null,
                        Message = "Initialized"
                    };
                }
                return orderby;
            }
            set
            {
                orderby = value;
                OnPropertyChanged(nameof(OrderBy));
            }
        }
    }
}
