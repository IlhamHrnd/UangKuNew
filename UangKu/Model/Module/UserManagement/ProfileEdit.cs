using System.Collections.ObjectModel;
using UangKu.Model.Base;
using UangKu.WebService.Data;
using static UangKu.WebService.Data.Location;

namespace UangKu.Model.Module.UserManagement
{
    public class ProfileEdit : BaseModel
    {
        private bool isenabled = false;
        public bool IsEnabled { get => isenabled; set => SetProperty(ref isenabled, value); }
        private ImageManager img;
        public ImageManager Img { get => img; set => img = value; }
        private Root<Profile.Data> person;
        public Root<Profile.Data> Person
        {
            get
            {
                if (person == null)
                {
                    person = new Root<Profile.Data>
                    {
                        Data = new Profile.Data(),
                        Succeeded = true,
                        Errors = null,
                        Message = "Initialized"
                    };
                }
                return person;
            }
            set
            {
                person = value;
                OnPropertyChanged(nameof(Person));
            }
        }
        private Task<Root<PostalCode>> postalcode;
        public Task<Root<PostalCode>> PostalCode { get => postalcode; set => postalcode = value; }

        #region Profile Variabel
        private Province selectedbirth { get; set; }
        public Province SelectedBirth
        {
            get { return selectedbirth; }
            set
            {
                if (selectedbirth != value)
                {
                    selectedbirth = value;
                    OnPropertyChanged(nameof(SelectedBirth));
                }
            }
        }
        private IList<Province> listbirth { get; set; }

        public IList<Province> ListBirth
        {
            get
            {
                if (listbirth == null)
                {
                    listbirth = new ObservableCollection<Province>();
                }
                return listbirth;
            }
            set { listbirth = value; }
        }

        private Province selectedprovince { get; set; }
        public Province SelectedProvince
        {
            get { return selectedprovince; }
            set
            {
                if (selectedprovince != value)
                {
                    selectedprovince = value;
                    OnPropertyChanged(nameof(SelectedProvince));
                }
            }
        }
        private IList<Province> listprovince { get; set; }

        public IList<Province> ListProvince
        {
            get
            {
                if (listprovince == null)
                {
                    listprovince = new ObservableCollection<Province>();
                }
                return listprovince;
            }
            set { listprovince = value; }
        }

        private City selectedcity { get; set; }
        public City SelectedCity
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
        private IList<City> listcity { get; set; }

        public IList<City> ListCity
        {
            get
            {
                if (listcity == null)
                {
                    listcity = new ObservableCollection<City>();
                }
                return listcity;
            }
            set { listcity = value; }
        }

        private District selecteddistrict { get; set; }
        public District SelectedDistrict
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
        private IList<District> listdistrict { get; set; }

        public IList<District> ListDistrict
        {
            get
            {
                if (listdistrict == null)
                {
                    listdistrict = new ObservableCollection<District>();
                }
                return listdistrict;
            }
            set { listdistrict = value; }
        }

        private SubDistrict selectedsubdistrict { get; set; }
        public SubDistrict SelectedSubdistrict
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
        private IList<SubDistrict> listsubdistrict { get; set; }

        public IList<SubDistrict> ListSubdistrict
        {
            get
            {
                if (listsubdistrict == null)
                {
                    listsubdistrict = new ObservableCollection<SubDistrict>();
                }
                return listsubdistrict;
            }
            set { listsubdistrict = value; }
        }
        #endregion
    }
}
