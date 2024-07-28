using System.Collections.ObjectModel;
using UangKu.Model.Base;
using UangKu.Model.Response.Location;
using static UangKu.Model.Response.Location.Cities;
using static UangKu.Model.Response.Location.District;
using static UangKu.Model.Response.Location.PostalCode;
using static UangKu.Model.Response.Location.Provinces;
using static UangKu.Model.Response.Location.Subdistrict;

namespace UangKu.Model.SubMenu
{
    public class EditProfile : BaseModel
    {
        private IList<Provinces.Datum> listprovinces { get; set; }

        public IList<Provinces.Datum> ListProvinces
        {
            get
            {
                if (listprovinces == null)
                {
                    listprovinces = new ObservableCollection<Provinces.Datum>();
                }
                return listprovinces;
            }
            set { listprovinces = value; }
        }
        private IList<Cities.Datum> listcities { get; set; }
        public IList<Cities.Datum> ListCities
        {
            get
            {
                if (listcities == null)
                {
                    listcities = new ObservableCollection<Cities.Datum>();
                }
                return listcities;
            }
            set { listcities = value; }
        }
        private IList<District.Datum> listdistricts { get; set; }
        public IList<District.Datum> ListDistricts
        {
            get
            {
                if (listdistricts == null)
                {
                    listdistricts = new ObservableCollection<District.Datum>();
                }
                return listdistricts;
            }
            set { listdistricts = value; }
        }
        private IList<Subdistrict.Datum> listsubdistricts { get; set; }
        public IList<Subdistrict.Datum> ListSubdistricts
        {
            get
            {
                if (listsubdistricts == null)
                {
                    listsubdistricts = new ObservableCollection<Subdistrict.Datum>();
                }
                return listsubdistricts;
            }
            set { listsubdistricts = value; }
        }
        private PostalCodeRoot postalcode { get; set; }
        public PostalCodeRoot PostalCode
        {
            get
            {
                if (postalcode == null)
                {
                    postalcode = new PostalCodeRoot();
                }
                return postalcode;
            }
            set { postalcode = value; }
        }
        private Provinces.Datum selectedplacebirth { get; set; }
        public Provinces.Datum SelectedPlaceBirth
        {
            get { return selectedplacebirth; }
            set
            {
                if (selectedplacebirth != value)
                {
                    selectedplacebirth = value;
                    OnPropertyChanged(nameof(SelectedPlaceBirth));
                }
            }
        }
        private Provinces.Datum selectedprovinces { get; set; }
        public Provinces.Datum SelectedProvinces
        {
            get { return selectedprovinces; }
            set
            {
                if (selectedprovinces != value)
                {
                    selectedprovinces = value;
                    OnPropertyChanged(nameof(SelectedProvinces));
                }
            }
        }
        private Cities.Datum selectedcity { get; set; }
        public Cities.Datum SelectedCity
        {
            get { return selectedcity; }
            set
            {
                if (selectedcity != value)
                {
                    selectedcity = value;
                    OnPropertyChanged(nameof(SelectedCity));
                }
            }
        }
        private District.Datum selecteddistrict { get; set; }
        public District.Datum SelectedDistrict
        {
            get { return selecteddistrict; }
            set
            {
                if (selecteddistrict != value)
                {
                    selecteddistrict = value;
                    OnPropertyChanged(nameof(SelectedDistrict));
                }
            }
        }
        private Subdistrict.Datum selectedsubdistrict { get; set; }
        public Subdistrict.Datum SelectedSubdistrict
        {
            get { return selectedsubdistrict; }
            set
            {
                if (selectedsubdistrict != value)
                {
                    selectedsubdistrict = value;
                    OnPropertyChanged(nameof(SelectedSubdistrict));
                }
            }
        }
    }
}
