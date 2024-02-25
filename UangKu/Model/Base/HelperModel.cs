﻿using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using SkiaSharp;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Text;
using UangKu.Model.Session;
using UangKu.ViewModel.RestAPI.AppParameter;
using UangKu.ViewModel.RestAPI.Picture;
using UangKu.ViewModel.RestAPI.Profile;
using UangKu.ViewModel.RestAPI.Report;
using UangKu.ViewModel.RestAPI.Transaction;
using static UangKu.Model.Base.ParameterModel;
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
                await MsgModel.MsgNotification(ItemDefaultValue.Offline);
            }
            else
            {
                await MsgModel.MsgNotification(ItemDefaultValue.Online);
            }
        }
    }

    public static class FormatCurrency
    {
        public static string Currency(decimal amount, string culture)
        {
            CultureInfo info = new CultureInfo(culture);
            return amount.ToString("C", info);
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

        [RequiresAssemblyFiles()]
        public static DateTime GetBuildDate()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var fileInfo = new FileInfo(assembly.Location);
            var lastWriteTime = fileInfo.LastWriteTime;
            return lastWriteTime;
        }

        public static string GetUserID(AppSession session)
        {
            if (!string.IsNullOrEmpty(session.personID))
            {
                return session.personID;
            }
            else if (string.IsNullOrEmpty(session.personID) && !string.IsNullOrEmpty(session.username))
            {
                return session.username;
            }
            else
            {
                return string.Empty;
            }
        }

        public static int GetUserAge(DateTime birthDate)
        {
            var now = DateTime.Now.Year;
            var date = birthDate.Year;
            var age = now - date;

            return age;
        }

        public static bool IsAdmin(string access)
        {
            bool admin = !string.IsNullOrEmpty(access) && access == "Admin";
            return admin;
        }

        public static bool IsAdult(int age)
        {
            bool adult = age >= Converter.StringToInt(AppParameter.AgeMinimum, AppParameterDefault.Age);
            return adult;
        }

        public static async void LoadProfile()
        {
            var profile = await GetProfile.GetProfileID(App.Session.personID);
            DateTime dateTime = profile.birthDate != null ? (DateTime)profile.birthDate : DateTime.Now;
            int AgeUser = GetUserAge(dateTime);
            App.Access = new AppAccess
            {
                IsAdmin = IsAdmin(App.Session.accessName),
                IsAdult = IsAdult(AgeUser)
            };
        }

        public static async void LoadAppParameter()
        {
            var allParameter = await AllParameterWithNoPageFilter.GetAllAppParameter();
            if (allParameter.Count > 0)
            {
                for (int i = 0; i < allParameter.Count; i++)
                {
                    var data = allParameter[i];
                    switch (data.parameterID)
                    {
                        case "MaxFileSize":
                            AppParameter.MaxFileSize = data.parameterValue;
                            break;

                        case "MaxPicture":
                            AppParameter.MaxPicture = data.parameterValue;
                            break;

                        case "AgeMinimum":
                            AppParameter.AgeMinimum = data.parameterValue;
                            break;

                        case "MaxResult":
                            AppParameter.MaxResult = data.parameterValue;
                            break;

                        case "HomeMaxResult":
                            AppParameter.HomeMaxResult = data.parameterValue;
                            break;

                        case "Timeout":
                            AppParameter.Timeout = data.parameterValue;
                            break;

                        case "ShowLastBuild":
                            AppParameter.ShowLastBuild = data.parameterValue;
                            break;

                        case "IsAllowCustomDate":
                            AppParameter.IsAllowCustomDate = data.parameterValue;
                            break;
                    }
                }
            }
        }
    }

    public static class DateFormat
    {
        public static string FormattingDate(DateTime date, string format)
        {
            string formattedDate = date.ToString(format);

            return formattedDate;
        }

        public static DateTime FormatYearMonthDateSplit(DateTime dateTime)
        {
            var date = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);

            return date;
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

        //Class Untuk Substring ContentType
        public static string SubstringContentType(string content, char type)
        {
            try
            {
                string result = string.Empty;
                int Index = content.LastIndexOf(type);

                if (Index != -1 && Index < content.Length - 1)
                {
                    result = content.Substring(Index + 1);
                }

                return result;
            }
            catch
            {
                return null;
            }
        }

        //Proses Upload Satu Gambar
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
            var intResult = Converter.StringToInt(AppParameter.MaxFileSize, Converter.StringToInt(AppParameter.MaxFileSize, AppParameterDefault.MaxFileSize));
            var longResult = Converter.IntToLong(intResult);

            if (fileSize > longResult)
            {
                await MsgModel.MsgNotification($"{result.FileName} Is More Than Limit");
                return null;
            }

            byte[] imgBytes;
            MemoryStream memorystream = new MemoryStream();
            await stream.CopyToAsync(memorystream);
            imgBytes = memorystream.ToArray();
            ImageManager.ImageByte = memorystream.ToArray();
            ImageManager.ImageString = ByteToStringImg(imgBytes);
            ImageManager.ImageName = result.FileName;
            ImageManager.ImageFormat = result.ContentType;

            return ImageSource.FromStream(() => new MemoryStream(imgBytes));
        }

        //Proses Upload Beberapa Gambar
        public static async Task<List<ImageManagerList>> PickMultipleImageAsync()
        {
            var result = await FilePicker.PickMultipleAsync(new PickOptions
            {
                PickerTitle = "Pick Image Please",
                FileTypes = FilePickerFileType.Images
            });

            if (result == null)
            {
                return null;
            }

            var ImageItems = new List<ImageManagerList>();

            foreach (var item in result)
            {
                var stream = await item.OpenReadAsync();

                long fileSize = stream.Length;
                var intResult = Converter.StringToInt(AppParameter.MaxFileSize, Converter.StringToInt(AppParameter.MaxFileSize, AppParameterDefault.MaxFileSize));
                var longResult = Converter.IntToLong(intResult);

                if (fileSize > longResult)
                {
                    await MsgModel.MsgNotification($"{item.FileName} Is More Than Limit");
                }
                else
                {
                    byte[] imgBytes;
                    MemoryStream memorystream = new MemoryStream();
                    await stream.CopyToAsync(memorystream);
                    imgBytes = memorystream.ToArray();

                    var images = new ImageManagerList
                    {
                        ImageString = ByteToStringImg(imgBytes),
                        ImageByte = memorystream.ToArray(),
                        ImageName = item.FileName,
                        ImageFormat = item.ContentType,
                        ImageSize = (int)fileSize
                    };

                    ImageItems.Add(images);
                }
            }

            return ImageItems;
        }
    }

    public static class Converter
    {
        public static int StringToInt(string dataString, int dataInt)
        {
            int result = !string.IsNullOrEmpty(dataString) ? int.Parse(dataString) : dataInt;

            return result;
        }

        public static long IntToLong(int data)
        {
            long result = data * 1024 * 1024;

            return result;
        }

        public static int DecimalToInt(decimal value)
        {
            var values = (int)value;
            return values;
        }

        public static bool StringToBool(string value)
        {
            bool result;

            try
            {
                result = Convert.ToBoolean(value);
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        //Function Untuk StringBuilder
        public static string BuilderString(params object[] items)
        {
            var builder = new StringBuilder();

            for (int i = 0; i < items.Length; i++)
            {
                builder.Append(items[i]);
            }
            var result = builder.ToString();
            return result;
        }
    }

    public static class Compare
    {
        public static bool StringCompare(string firstString, string secondString)
        {
            bool result = firstString == secondString;

            return result;
        }
    }

    public static class RandomColorGenerator
    {
        private static readonly Random random = new Random();

        public static Color RGBGenerateRandomColor()
        {
            byte red = (byte)random.Next(256);
            byte green = (byte)random.Next(256);
            byte blue = (byte)random.Next(256);

            var color = Color.FromRgb(red, green, blue);

            return color;
        }

        public static string HexGenerateRandomColor()
        {
            byte[] colorBytes = new byte[3];

            random.NextBytes(colorBytes);
            string hexColor = $"#{colorBytes[0]:X2}{colorBytes[1]:X2}{colorBytes[2]:X2}";

            return hexColor;
        }

        public static SKColor SKGenerateRandomColor()
        {
            byte[] colorBytes = new byte[3];
            random.NextBytes(colorBytes);

            var skcolor = new SKColor(colorBytes[0], colorBytes[1], colorBytes[2]);

            return skcolor;
        }
    }

    public static class GetNewAutoNumber
    {
        public static async Task<string> GetPictureID()
        {
            try
            {
                string pictureID = string.Empty;
                var generateID = await NewPictureID.GetNewPictureID(ItemDefaultValue.Upload);
                pictureID = !string.IsNullOrEmpty(generateID) ? generateID : string.Empty;
                return pictureID;
            }
            catch (Exception e)
            {
                await MsgModel.MsgNotification($"Error: {e.Message}");
                return null;
            }
        }

        public static async Task<string> GetTransactionNo(string transType)
        {
            try
            {
                string transNo = string.Empty;
                var generateID = await NewTransNo.GetNewTransNo(transType);
                transNo = !string.IsNullOrEmpty(generateID) ? generateID : string.Empty;
                return transNo;
            }
            catch (Exception e)
            {
                await MsgModel.MsgNotification($"Error: {e.Message}");
                return null;
            }
        }

        public static async Task<string> GetReportNo(string reportType)
        {
            try
            {
                string reportNo = string.Empty;
                var generateID = await NewReportNo.GetNewReportNo(reportType);
                reportNo = !string.IsNullOrEmpty(generateID) ? generateID : string.Empty;
                return reportNo;
            }
            catch (Exception e)
            {
                await MsgModel.MsgNotification($"Error: {e.Message}");
                return null;
            }
        }

        public static string GenerateUserReportNo(string userID, string accessName)
        {
            string value = string.Empty;
            if (!string.IsNullOrEmpty(userID) && !string.IsNullOrEmpty(accessName))
            {
                value = string.Concat(userID, GenerateAccesNameForReportNo(accessName));
            }

            return value;
        }

        public static string GenerateAccesNameForReportNo(string accessName)
        {
            string value = string.Empty;
            if (!string.IsNullOrEmpty(accessName))
            {
                switch (accessName)
                {
                    case "Admin":
                        value = accessName.Substring(0, 3);
                        break;
                    case "User":
                        {
                            var accessStartSplit = accessName.Substring(0, 2);
                            var accessEndSplit = accessName.Substring(accessName.Length - 1);
                            value = string.Concat(accessStartSplit, accessEndSplit);
                            break;
                        }

                    default:
                        //Temporary Jika Akses User Bukan Admin Atau User
                        value = "TEM";
                        break;
                }
            }
            return value;
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
        //Class Untuk Pengecekan Entry Yang Null Karna Data Harus Di Isi Semua
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

        //Class Untuk Pengecekan Picker Yang Null Karna Data Harus Di Isi Semua
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
