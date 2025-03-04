using System.Threading.Tasks;
using UangKu.Model.Base;
using UangKu.Model.Module.Wishlist;

namespace UangKu.ViewModel.Module.Wishlist
{
    public class WishlistEditVM : WishlistEdit
    {
        public WishlistEditVM(string mode, string transNo)
        {
            Mode = mode;
            TransNo = transNo;
        }

        public async Task LoadData()
        {
            if (!Network.IsConnected)
                return;

            if (string.IsNullOrEmpty(Mode))
                return;

            if (Mode == ItemManager.EditFile && string.IsNullOrEmpty(TransNo))
                return;

            IsBusy = true;
            AppProgram(Mode == ItemManager.EditFile ? Model.Base.AppProgram.WishlistEdit : Model.Base.AppProgram.WishlistNew);

            try
            {

            }
            catch (Exception e)
            {
                await MsgModel.MsgNotification(e.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
