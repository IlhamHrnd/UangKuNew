﻿using Mopups.Services;
using UangKu.Model.Base;
using UangKu.Model.Menu;

namespace UangKu.ViewModel.Menu
{
    public class AppStandardReferenceVM : AppStandardReference
    {
        private NetworkModel network = NetworkModel.Instance;
        public AppStandardReferenceVM()
        {
            
        }
        public async void LoadData(int pageNumber, int pageSize)
        {
            bool isConnect = network.IsConnected;
            IsBusy = true;
            try
            {
                if (!isConnect)
                {
                    await MsgModel.MsgNotification("You're Offline");
                }
                var asr = await RestAPI.AppStandardReferenceItem.AppStandardReference.GetAllASR(pageNumber, pageSize);
                if (asr.data.Count > 0)
                {
                    ListASR.Clear();
                    ListASR.Add(asr);
                    Page = (int)asr.pageNumber;
                }
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
        public async void NextPage_Clicked(int pageSize)
        {
            var maxPage = ListASR[0].totalPages;
            bool isConnect = network.IsConnected;
            try
            {
                if (!isConnect)
                {
                    await MsgModel.MsgNotification("You're Offline");
                }
                if (Page >= maxPage)
                {
                    await MsgModel.MsgNotification("This Is The Latest Page");
                }
                else
                {
                    var asr = await RestAPI.AppStandardReferenceItem.AppStandardReference.GetAllASR(Page + 1, pageSize);
                    if ((bool)asr.succeeded)
                    {
                        ListASR.Clear();
                        ListASR.Add(asr);
                        Page = (int)asr.pageNumber;
                    }
                }
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
        public async void PreviousPage_Click(int pageSize)
        {
            bool isConnect = network.IsConnected;
            try
            {
                if (!isConnect)
                {
                    await MsgModel.MsgNotification("You're Offline");
                }
                if (Page <= 1)
                {
                    await MsgModel.MsgNotification("This Is The First Page");
                }
                else
                {
                    var asr = await RestAPI.AppStandardReferenceItem.AppStandardReference.GetAllASR(Page - 1, pageSize);
                    if ((bool)asr.succeeded)
                    {
                        ListASR.Clear();
                        ListASR.Add(asr);
                        Page = (int)asr.pageNumber;
                    }
                }
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
        public async Task AppStandardReferenceItem_PopUp(SelectionChangedEventArgs args)
        {
            var standardID = args.CurrentSelection[0] as Model.Response.AppStandardReference.AppStandardReference.Datum;

            if (standardID != null)
            {
                var id = standardID.standardReferenceID;
                var asri = new View.Menu.AppStandardReferenceItem(id);
                await MopupService.Instance.PushAsync(asri);
            }
            else
            {
                await MsgModel.MsgNotification($"You Haven't Selected An Item Yet");
            }
        }
    }
}
