﻿using System.Collections.ObjectModel;
using UangKu.Model.Base;
using UangKu.Model.Response.Transaction;
using static UangKu.Model.Response.Picture.UserPicture;
using static UangKu.Model.Response.Transaction.AllTransaction;

namespace UangKu.Model.Menu
{
    public class Home : BaseModel
    {
        public Home()
        {
            
        }
        private string name = string.Empty;
        public string Name { get => name; set => name = value; }
        private string person = string.Empty;
        public string Person { get => person; set => person = value; }
        private string image = string.Empty;
        public string Image { get => image; set => image = value; }
        public string month = string.Empty;
        public string Month { get => month; set => month = value; }
        private IList<UserPictureRoot> listuserpicture { get; set; }
        public IList<UserPictureRoot> ListUserPicture
        {
            get
            {
                if (listuserpicture == null)
                {
                    listuserpicture = new ObservableCollection<UserPictureRoot>();
                }
                return listuserpicture;
            }
            set { listuserpicture = value; }
        }
        private IList<SumTransaction.Datum> listsumtrans { get; set; }

        public IList<SumTransaction.Datum> ListSumTrans
        {
            get
            {
                if (listsumtrans == null)
                {
                    listsumtrans = new ObservableCollection<SumTransaction.Datum>();
                }
                return listsumtrans;
            }
            set { listsumtrans = value; }
        }
        private IList<AllTransactionRoot> listalltrans { get; set; }

        public IList<AllTransactionRoot> ListAllTrans
        {
            get
            {
                if (listalltrans == null)
                {
                    listalltrans = new ObservableCollection<AllTransactionRoot>();
                }
                return listalltrans;
            }
            set { listalltrans = value; }
        }
    }
}
