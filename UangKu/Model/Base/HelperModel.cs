using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Storage;
using Syncfusion.Maui.Toolkit.Themes;
using System.Globalization;
using System.Text;
using UangKu.Model.Session;
using static UangKu.Model.Base.PermissionManager;

namespace UangKu.Model.Base
{
    public static class MsgModel
    {
        public static async Task MsgNotification(string message)
        {
            var toast = Toast.Make(message, ToastDuration.Long);
            await toast.Show();
        }

        public static async Task MsgNotification(string message, CancellationToken token)
        {
            var toast = Toast.Make(message, ToastDuration.Long);
            await toast.Show(token);
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
                await MsgModel.MsgNotification(ItemManager.Offline);
            }
            else
            {
                await MsgModel.MsgNotification(ItemManager.Online);
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

        public static string GetUserAge(DateTime dateTime)
        {
            DateTime today = DateTime.Today;

            // Calculate years, months, and days
            int years = today.Year - dateTime.Year;
            int months = today.Month - dateTime.Month;
            int days = today.Day - dateTime.Day;

            // Adjust for negative values
            if (days < 0)
            {
                months--;
                days += DateTime.DaysInMonth(today.Year, (today.Month == 1) ? 12 : today.Month - 1);
            }

            if (months < 0)
            {
                years--;
                months += 12;
            }

            // Return formatted age
            return $"{years} year{(years != 1 ? "s" : "")} {months} month{(months != 1 ? "s" : "")} {days} day{(days != 1 ? "s" : "")}";
        }

        public static string GetUserID()
        {
            var session = App.Session;
            if (!string.IsNullOrEmpty(session.personID))
                return session.personID;
            else if (string.IsNullOrEmpty(session.personID) && !string.IsNullOrEmpty(session.username))
                return session.username;
            else
                return string.Empty;
        }

        public static async Task<bool> LoadAppParameterAsync()
        {
            var allParameter = await WebService.Service.AppParameter.GetAllParameterWithNoPageFilter();
            if (allParameter.Succeeded == true)
            {
                foreach (var data in allParameter.Data)
                {
                    try
                    {
                        if (bool.TryParse(data.parameterValue, out var valueBool))
                        {
                            switch (data.parameterId)
                            {
                                case "ShowLastBuild":
                                    AppParameter.ShowLastBuild = valueBool;
                                    break;

                                case "IsAllowCustomDate":
                                    AppParameter.IsAllowCustomDate = valueBool;
                                    break;

                                case "IsAddPageNumber":
                                    AppParameter.IsAddPageNumber = valueBool;
                                    break;

                                case "IsAddHeader":
                                    AppParameter.IsAddHeader = valueBool;
                                    break;

                                case "IsAddFooter":
                                    AppParameter.IsAddFooter = valueBool;
                                    break;
                            }
                        }
                        else if (int.TryParse(data.parameterValue, out var valueInt))
                        {
                            switch (data.parameterId)
                            {
                                case "AgeMinimum":
                                    AppParameter.AgeMinimum = valueInt;
                                    break;

                                case "MaxFileSize":
                                    AppParameter.MaxFileSize = valueInt;
                                    break;

                                case "MaxPicture":
                                    AppParameter.MaxPicture = valueInt;
                                    break;

                                case "MaxResult":
                                    AppParameter.MaxResult = valueInt;
                                    break;

                                case "HomeMaxResult":
                                    AppParameter.HomeMaxResult = valueInt;
                                    break;

                                case "Timeout":
                                    AppParameter.Timeout = valueInt;
                                    break;
                            }
                        }
                        else
                        {
                            switch (data.parameterId)
                            {
                                case "URL":
                                    AppParameter.URL = data.parameterValue;
                                    break;

                                case "NumericFormat":
                                    AppParameter.NumericFormat = data.parameterValue;
                                    break;

                                case "CurrencyFormat":
                                    AppParameter.CurrencyFormat = data.parameterValue;
                                    break;

                                case "DownloadFolder":
                                    AppParameter.DownloadFolder = data.parameterValue;
                                    break;

                                case "BlankPDF":
                                    AppParameter.BlankPDF = data.parameterValue;
                                    break;

                                case "PDFPageSize":
                                    AppParameter.PDFPageSize = data.parameterValue;
                                    break;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        await MsgModel.MsgNotification(e.Message);
                    }
                }
                return true;
            }
            else
                await MsgModel.MsgNotification(allParameter.Message);

            return false;
        }

        public static void LogError(Exception ex)
        {
            // Log to a file, database, or remote API
            Console.WriteLine($"[Error]: {ex.Message}");

            // Optionally show the error to the user
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await MsgModel.MsgNotification(ex.Message);
            });
        }
    }

    public static class DateFormat
    {
        public static string FormattingDate(DateTime date, string format)
        {
            string formattedDate = date.ToString(format);

            return formattedDate;
        }

        public static DateTime FormattingDateSplit(int year, int month, int day)
        {
            if (month == 1)
            {
                year--;
                month = 12;
            }

            var date = new DateTime(year, month, day);

            return date;
        }

        public static DateTime AddDays(int days, DateTime dateTime)
        {
            var day = dateTime.AddDays(days);

            return day;
        }

        private static DateTime datetime = DateTime.Now;
        public static DateTime DateTime { get => datetime; set => datetime = value; }
    }

    public static class NumericFormat
    {
        public static string NumberDigit(int number, string format)
        {
            string formattedNumber = number.ToString(format);
            return formattedNumber;
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
            var intResult = AppParameter.MaxFileSize;
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
            ImageManager.ImageString = Converter.ByteToStringImg(imgBytes);
            ImageManager.ImageName = result.FileName;
            ImageManager.ImageFormat = result.ContentType;
            ImageManager.ImageSize = fileSize;

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
                var intResult = AppParameter.MaxFileSize;
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
                        ImageString = Converter.ByteToStringImg(imgBytes),
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

        public static string IntToString(int data)
        {
            var result = data.ToString();
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

        public static string DecimalToString(decimal value)
        {
            try
            {
                int result = (int)Math.Round(value);
                var values = result.ToString();
                return values;
            }
            catch (OverflowException)
            {
                return "Value too large";
            }
        }

        public static bool StringToBool(string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value)) 
                    return false;

                if (value == "1" || value.Equals("yes", StringComparison.CurrentCultureIgnoreCase) || value.Equals("true", StringComparison.CurrentCultureIgnoreCase))
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public static DateTime StringToDateTime(string value)
        {
            try
            {
                return DateTime.Parse(value);
            }
            catch
            {
                return new DateTime();
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

        //Function Untuk Convert PDF Ke Byte[]
        public static byte[] PDFToByte(string filePath)
        {
            byte[] pdfBytes = File.ReadAllBytes(filePath);
            return pdfBytes;
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

        //Function Untuk IList Jadi List
        public static List<T> ConvertIListToList<T>(IList<T> inputList)
        {
            var list = new List<T>(inputList);
            return list;
        }
    }

    public static class FileHelper
    {
        public static string GetFilePath(string fileName)
        {
            string result;
            #if ANDROID
		        var docsDirectory = Android.App.Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryDocuments);
		        result = Path.Combine(docsDirectory.AbsoluteFile.Path, fileName);
            #else
                result = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);
            #endif
            return result;
        }

        public static bool CopyFile(string sourcePath, string destinationPath, bool isReplace)
        {
            try
            {
                File.Copy(sourcePath, destinationPath, isReplace);
                return true;
            }
            catch (Exception e)
            {
                _ = MsgModel.MsgNotification($"{e.Message}");
                return false;
            }
        }

        public static async Task SaveFile(string fileName, byte[] pdfBytes, CancellationToken token)
        {
            var stream = new MemoryStream(pdfBytes);
            var fileSaverResult = await FileSaver.SaveAsync(fileName, stream, token);

            if (fileSaverResult.IsSuccessful)
            {
                await MsgModel.MsgNotification($"PDF file saved successfully at: {fileSaverResult.FilePath}", token);
            }
            else
            {
                await MsgModel.MsgNotification($"Failed to save PDF file: {fileSaverResult.Exception.Message}", token);
            }
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
    }

    public static class ControlHelper
    {
        public static int GetIndexByName<T>(List<T> itemList, Func<T, string> nameSelector, string targetName)
        {
            // Find the index using LINQ
            int selectedIndex = itemList.FindIndex(item => nameSelector(item) == targetName);
            return selectedIndex;
        }

        public async static void OnPopNavigationAsync(INavigation navigation)
        {
            try
            {
                if (navigation.NavigationStack.Count > 1)
                {
                    await navigation.PopAsync();
                }
                else
                {
                    await MsgModel.MsgNotification("No previous page in the stack");
                }
            }
            catch (Exception e)
            {
                await MsgModel.MsgNotification(e.Message);
            }
        }

        public static void OnChangeTheme()
        {
            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;

            if (mergedDictionaries != null)
            {
                var theme = mergedDictionaries.OfType<SyncfusionThemeResourceDictionary>().FirstOrDefault();

                if (theme != null)
                {
                    // Detect system theme
                    AppTheme systemTheme = Application.Current.RequestedTheme;

                    if (systemTheme == AppTheme.Dark)
                    {
                        theme.VisualTheme = SfVisuals.MaterialDark;
                        Application.Current.UserAppTheme = AppTheme.Dark;
                    }
                    else if (systemTheme == AppTheme.Light)
                    {
                        theme.VisualTheme = SfVisuals.MaterialLight;
                        Application.Current.UserAppTheme = AppTheme.Light;
                    }
                    else
                    {
                        // Default behavior (toggle if unspecified)
                        if (theme.VisualTheme is SfVisuals.MaterialDark)
                        {
                            theme.VisualTheme = SfVisuals.MaterialLight;
                            Application.Current.UserAppTheme = AppTheme.Light;
                        }
                        else
                        {
                            theme.VisualTheme = SfVisuals.MaterialDark;
                            Application.Current.UserAppTheme = AppTheme.Dark;
                        }
                    }
                }
            }
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

                case PermissionType.StorageWrite:
                    status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
                    break;
            }

            if (status != PermissionStatus.Granted)
            {
                switch (permissionType)
                {
                    case PermissionType.StorageRead:
                        status = await Permissions.RequestAsync<Permissions.StorageRead>();
                        break;

                    case PermissionType.StorageWrite:
                        status = await Permissions.RequestAsync<Permissions.StorageWrite>();
                        break;
                }
            }

            await MsgModel.MsgNotification($"{permissionType} Is {status}");
        }

        public static async Task<bool> CheckPermissionAsync(PermissionType type)
        {
            var status = PermissionStatus.Unknown;

            switch (type)
            {
                case PermissionType.StorageRead:
                    status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
                    break;

                case PermissionType.StorageWrite:
                    status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
                    break;
            }

            bool isGranted = status == PermissionStatus.Granted;
            return isGranted;
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
