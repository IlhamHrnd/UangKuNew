using UangKu.Model.Base;
using UangKu.WebService.Data;

namespace UangKu.Model.Menu
{
    public class Profile : BaseModel
    {
        private Root<WebService.Data.Profile.Data> person;
        public Root<WebService.Data.Profile.Data> Person
        {
            get
            {
                if (person == null)
                {
                    person = new Root<WebService.Data.Profile.Data>
                    {
                        Data = new WebService.Data.Profile.Data(),
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
    }
}
