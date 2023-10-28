﻿using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System.Text;
using static UangKu.Model.Base.ParameterModel.PermissionManager;

namespace UangKu.Model.Base
{
    public static class MsgModel
    {
        public static async Task MsgNotification(string message)
        {
            var toast = Toast.Make(message, ToastDuration.Long);
            await toast.Show();
        }
    }

    public class NetworkModel
    {
        private static readonly NetworkModel network = new NetworkModel();
        public static NetworkModel Instance => network;
        public bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        public NetworkModel()
        {
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }
        //Kondisi Saat Koneksi Berubah
        private async void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (!IsConnected)
            {
                await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
            }
            else
            {
                await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Online);
            }
        }
    }

    public static class SessionModel
    {
        //Pengecekan Session Saat Sudah Login
        public static async Task<bool> SessionCheck()
        {
            if (App.Session == null)
            {
                await MsgModel.MsgNotification("Your session has expired");
                var shell = new AppShell();
                App.Current.MainPage = shell;
                return false;
            }
            return true;
        }
    }

    public static class ImageConvert
    {
        //Class Untuk Mengambil Gambar Byte[] Dari Response WebService
        public static ImageSource ImgByte(byte[] imgPath)
        {
            try
            {
                ImageSource source = ImageSource.FromStream(() => new MemoryStream(imgPath));
                return source;
            }
            catch (Exception e)
            {
                _ = MsgModel.MsgNotification($"{e.Message}");
                return null;
            }
        }

        //Class Untuk Convert Byte[] Ke Base64String
        public static string ByteToStringImg(byte[] imgPath)
        {
            try
            {
                string imgString = Convert.ToBase64String(imgPath);
                return imgString;
            }
            catch (Exception e)
            {
                _ = MsgModel.MsgNotification($"{e.Message}");
                return null;
            }
        }

        //Class Untuk Convert Base64String Ke Byte[]
        public static byte[] StringToByteImg(string imgPath)
        {
            try
            {
                byte[] imgByte = Convert.FromBase64String(imgPath);
                return imgByte;
            }
            catch (Exception e)
            {
                _ = MsgModel.MsgNotification($"{e.Message}");
                return null;
            }
        }

        //Class Untuk Decode Base64 Ke Byte[]
        public static byte[] DecodeBase64ToBytes(string base64String)
        {
            try
            {
                byte[] data = Convert.FromBase64String(base64String);
                return data;
            }
            catch (Exception e)
            {
                _ = MsgModel.MsgNotification($"{e.Message}");
                return null;
            }
        }

        //Class Untuk Decode Base64 Ke String
        public static string DecodeBase64ToString(string base64String)
        {
            try
            {
                byte[] data = Convert.FromBase64String(base64String);
                string decodedString = Encoding.UTF8.GetString(data);
                return decodedString;
            }
            catch (Exception e)
            {
                _ = MsgModel.MsgNotification($"{e.Message}");
                return null;
            }
        }

        //Proses Upload Gambar
        public static async Task<ImageSource> PickImageAsync()
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Pick Image Please",
                FileTypes = FilePickerFileType.Images
            });

            if (result == null)
            {
                return null;
            }

            var stream = await result.OpenReadAsync();

            long fileSize = stream.Length;

            if (fileSize > ParameterModel.ItemDefaultValue.MaxFileSize)
            {
                await MsgModel.MsgNotification($"{result.FileName} Is More Than Limit");
                return null;
            }

            byte[] imgBytes;
            MemoryStream memorystream = new MemoryStream();
            await stream.CopyToAsync(memorystream);
            imgBytes = memorystream.ToArray();
            ParameterModel.ImageManager.ImageByte = memorystream.ToArray();
            ParameterModel.ImageManager.ImageString = ByteToStringImg(imgBytes);

            return ImageSource.FromStream(() => new MemoryStream(imgBytes));
        }
    }

    public static class PermissionRequest
    {
        //Class Untuk Request Permission
        public static async Task RequestPermission(PermissionType permissionType)
        {
            PermissionStatus status = PermissionStatus.Unknown;

            switch (permissionType)
            {
                case PermissionType.StorageRead:
                    status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
                    break;
            }

            if (status != PermissionStatus.Granted)
            {
                switch (permissionType)
                {
                    case PermissionType.StorageRead:
                        status = await Permissions.RequestAsync<Permissions.StorageRead>();
                        break;
                }
            }

            await MsgModel.MsgNotification($"{permissionType} Is {status}");
        }
    }

    public static class ValidateNullChecker
    {
        public static async Task<bool> EntryValidateFields(params (string FieldValue, string FieldName)[] fields)
        {
            var errorMessages = new List<string>();

            foreach (var (value, name) in fields)
            {
                if (string.IsNullOrEmpty(value))
                {
                    errorMessages.Add(name);
                }
            }

            if (errorMessages.Count > 0)
            {
                var errorMessage = "The Following Data Are Required:\n";
                errorMessage += string.Join("\n", errorMessages);
                await MsgModel.MsgNotification(errorMessage);
                return false;
            }

            return true;
        }

        public static async Task<bool> PickerValidateFields(params (Picker Picker, string FieldName)[] pickers)
        {
            var errorMessages = new List<string>();

            foreach (var (picker, fieldName) in pickers)
            {
                if (picker.SelectedItem == null)
                {
                    errorMessages.Add(fieldName);
                }
            }

            if (errorMessages.Count > 0)
            {
                var errorMessage = "The Following Fields Are Required:\n";
                errorMessage += string.Join("\n", errorMessages);
                await MsgModel.MsgNotification(errorMessage);
                return false;
            }

            return true;
        }
    }
}
