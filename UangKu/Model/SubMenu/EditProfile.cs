using System.Collections.ObjectModel;
using UangKu.Model.Base;
using static UangKu.Model.Response.Location.Cities;
using static UangKu.Model.Response.Location.District;
using static UangKu.Model.Response.Location.PostalCode;
using static UangKu.Model.Response.Location.Provinces;
using static UangKu.Model.Response.Location.Subdistrict;

namespace UangKu.Model.SubMenu
{
    public class EditProfile : BaseModel
    {
        private IList<ProvincesRoot> listprovinces { get; set; }

        public IList<ProvincesRoot> ListProvinces
        {
            get
            {
                if (listprovinces == null)
                {
                    listprovinces = new ObservableCollection<ProvincesRoot>();
                }
                return listprovinces;
            }
            set { listprovinces = value; }
        }
        private IList<CitiesRoot> listcities { get; set; }
        public IList<CitiesRoot> ListCities
        {
            get
            {
                if (listcities == null)
                {
                    listcities = new ObservableCollection<CitiesRoot>();
                }
                return listcities;
            }
            set { listcities = value; }
        }
        private IList<DistrictRoot> listdistricts { get; set; }
        public IList<DistrictRoot> ListDistricts
        {
            get
            {
                if (listdistricts == null)
                {
                    listdistricts = new ObservableCollection<DistrictRoot>();
                }
                return listdistricts;
            }
            set { listdistricts = value; }
        }
        private IList<SubdistrictRoot> listsubdistricts { get; set; }
        public IList<SubdistrictRoot> ListSubdistricts
        {
            get
            {
                if (listsubdistricts == null)
                {
                    listsubdistricts = new ObservableCollection<SubdistrictRoot>();
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
        private ProvincesRoot selectedplacebirth { get; set; }
        public ProvincesRoot SelectedPlaceBirth
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
        private ProvincesRoot selectedprovinces { get; set; }
        public ProvincesRoot SelectedProvinces
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
        private CitiesRoot selectedcity { get; set; }
        public CitiesRoot SelectedCity
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
        private DistrictRoot selecteddistrict { get; set; }
        public DistrictRoot SelectedDistrict
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
        private SubdistrictRoot selectedsubdistrict { get; set; }
        public SubdistrictRoot SelectedSubdistrict
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
