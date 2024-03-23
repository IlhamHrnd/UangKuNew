﻿using System.Collections.ObjectModel;
using UangKu.Model.Base;
using static UangKu.Model.Response.AppStandardReference.AppStandardReferenceID;
using static UangKu.Model.Response.AppStandardReferenceItem.AppStandardReferenceItem;

namespace UangKu.Model.SubMenu
{
    public class AddAppStandardReferenceModel : BaseModel
    {
        private bool isedit = false;
        public bool IsEdit { get => isedit; set => SetProperty(ref isedit, value); }
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
