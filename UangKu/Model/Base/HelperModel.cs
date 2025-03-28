﻿using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Storage;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
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
using UangKu.ViewModel.RestAPI.Wishlist;
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

        public static string GetAppName()
        {
            var appName = AppInfo.Current.Name;
            return appName;
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

        public static string GetUserAccess(AppSession session)
        {
            var access = !string.IsNullOrEmpty(session.accessName) ? session.accessName : string.Empty;
            return access;
        }

        public static bool IsAdmin(string access)
        {
            bool admin = !string.IsNullOrEmpty(access) && access == "Admin";
            return admin;
        }

        public static bool IsAdult(int age)
        {
            bool adult = age >= AppParameter.AgeMinimum;
            return adult;
        }

        public static async void LoadProfile()
        {
            var profile = await GetProfile.GetProfileID(App.Session.personID);
            if (profile.metaData.isSucces && profile.metaData.code == 200)
            {
                DateTime dateTime = profile.birthDate != null ? (DateTime)profile.birthDate : DateTime.Now;
                int AgeUser = GetUserAge(dateTime);
                App.Access = new AppAccess
                {
                    IsAdmin = IsAdmin(App.Session.accessName),
                    IsAdult = IsAdult(AgeUser)
                };
            }
            else
            {
                await MsgModel.MsgNotification(profile.metaData.message);
            }
        }

        public static async void LoadAppParameter()
        {
            var allParameter = await AllParameterWithNoPageFilter.GetAllAppParameter();
            if (allParameter.metaData.isSucces && allParameter.metaData.code == 200 && allParameter.data.Count > 0)
            {
                foreach (var data in allParameter.data)
                {
                    try
                    {
                        if (bool.TryParse(data.parameterValue, out var valueBool))
                        {
                            switch (data.parameterID)
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
                            switch (data.parameterID)
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
                            switch (data.parameterID)
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
                        await MsgModel.MsgNotification($"{e.Message}");
                    }
                }
            }
            else
            {
                await MsgModel.MsgNotification(allParameter.metaData.message);
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

        public static bool StringToBool(string value, bool defaultValue)
        {
            bool result;

            try
            {
                result = Convert.ToBoolean(value);
            }
            catch (Exception)
            {
                result = defaultValue;
            }

            return result;
        }

        public static DateTime StringToDateTime(string value, DateTime defaultValue)
        {
            DateTime result;

            try
            {
                result = DateTime.Parse(value);
            }
            catch
            {
                result = defaultValue;
            }

            return result;
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

    public static class GeneratePDFFile
    {
        public static Paragraph SetParagraph(string title, int size, iText.Layout.Properties.TextAlignment alignment)
        {
            Paragraph paragraph = new Paragraph(title)
                .SetTextAlignment(alignment)
                .SetFontSize(size);
            return paragraph;
        }

        public static LineSeparator SetLine()
        {
            LineSeparator ls = new LineSeparator(new SolidLine());
            return ls;
        }

        public static Paragraph SetNewLine()
        {
            Paragraph paragraph = new Paragraph(new Text("\n"));
            return paragraph;
        }

        public static iText.Layout.Element.Image SetImage(string imgPath, iText.Layout.Properties.TextAlignment alignment)
        {
            var img = new iText.Layout.Element.Image(ImageDataFactory
               .Create(imgPath))
               .SetTextAlignment(alignment);
            return img;
        }

        public static Link SetLink(string title, string url)
        {
            var link = new Link(title,
                PdfAction.CreateURI(url));
            return link;
        }

        public static Table SetTable(int column, bool isLarge)
        {
            var tbl = new Table(column, isLarge)
                .SetKeepTogether(false);
            return tbl;
        }

        public static iText.Layout.Element.Cell SetCell(int rowsSpan, bool isAddBGColor, string title,
            iText.Layout.Properties.TextAlignment alignment)
        {
            var cell = new iText.Layout.Element.Cell(rowsSpan, rowsSpan)
                .SetTextAlignment(alignment)
                .Add(new Paragraph(title));

            if (isAddBGColor)
            {
                cell.SetBackgroundColor(iText.Kernel.Colors.ColorConstants.GRAY);
            }

            return cell;
        }

        public static iText.Kernel.Geom.PageSize SetPageSize(string size)
        {
            var pageSize = new Dictionary<string, iText.Kernel.Geom.PageSize>(StringComparer.OrdinalIgnoreCase)
            {
                { "A0", iText.Kernel.Geom.PageSize.A0 },
                { "A1", iText.Kernel.Geom.PageSize.A1 },
                { "A2", iText.Kernel.Geom.PageSize.A2 },
                { "A3", iText.Kernel.Geom.PageSize.A3 },
                { "A4", iText.Kernel.Geom.PageSize.A4 },
                { "A5", iText.Kernel.Geom.PageSize.A5 },
                { "A6", iText.Kernel.Geom.PageSize.A6 },
                { "A7", iText.Kernel.Geom.PageSize.A7 },
                { "A8", iText.Kernel.Geom.PageSize.A8 },
                { "A9", iText.Kernel.Geom.PageSize.A9 },
                { "A10", iText.Kernel.Geom.PageSize.A10 },
                { "B0", iText.Kernel.Geom.PageSize.B0 },
                { "B1", iText.Kernel.Geom.PageSize.B1 },
                { "B2", iText.Kernel.Geom.PageSize.B2 },
                { "B3", iText.Kernel.Geom.PageSize.B3 },
                { "B4", iText.Kernel.Geom.PageSize.B4 },
                { "B5", iText.Kernel.Geom.PageSize.B5 },
                { "B6", iText.Kernel.Geom.PageSize.B6 },
                { "B7", iText.Kernel.Geom.PageSize.B7 },
                { "B8", iText.Kernel.Geom.PageSize.B8 },
                { "B9", iText.Kernel.Geom.PageSize.B9 },
                { "B10", iText.Kernel.Geom.PageSize.B10 },
                { "executive", iText.Kernel.Geom.PageSize.EXECUTIVE },
                { "ledger", iText.Kernel.Geom.PageSize.LEDGER },
                { "legal", iText.Kernel.Geom.PageSize.LEGAL },
                { "letter", iText.Kernel.Geom.PageSize.LETTER },
                { "tabloid", iText.Kernel.Geom.PageSize.TABLOID }
            };
            var pages = pageSize.TryGetValue(size, out var page) ? page : iText.Kernel.Geom.PageSize.A4;
            return pages;
        }

        public static void SetPagesNumber(PdfDocument pdfDoc, Document doc)
        {
            int n = pdfDoc.GetNumberOfPages();
            for (int i = 1; i <= n; i++)
            {
                doc.ShowTextAligned(new Paragraph($"Page " + i + " of " + n),
                   559, 806, i, iText.Layout.Properties.TextAlignment.RIGHT,
                   iText.Layout.Properties.VerticalAlignment.TOP, 0);
            }
        }

        public static void SetFooterPages(Paragraph par, PdfDocument pdfdoc, Document doc)
        {
            for (int i = 1; i <= pdfdoc.GetNumberOfPages(); i++)
            {
                PdfPage pdfPage = pdfdoc.GetPage(i);
                pdfPage.SetIgnorePageRotationForContent(true);

                var pageSize = pdfPage.GetPageSize();
                float x;
                float y;
                if (pdfPage.GetRotation() % 180 == 0)
                {
                    x = pageSize.GetWidth() / 2;
                    y = pageSize.GetBottom() + 20;
                }
                else
                {
                    x = pageSize.GetHeight() / 2;
                    y = pageSize.GetRight() - 20;
                }

                doc.ShowTextAligned(par, x, y, i, iText.Layout.Properties.TextAlignment.CENTER, iText.Layout.Properties.VerticalAlignment.BOTTOM, 0);
            }
        }
    }

    public static class Compare
    {
        public static bool StringCompare(string firstString, string secondString)
        {
            bool result = firstString == secondString;
            return result;
        }

        public static string StringReplace(string valueString, string defaultString)
        {
            string result = string.IsNullOrEmpty(valueString) ? defaultString : valueString;
            return result;
        }

        public static int IntReplace(int valueInt, int defaultInt)
        {
            int result = valueInt > 0 ? valueInt : defaultInt;
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
                pictureID = generateID.metaData.isSucces && generateID.metaData.code == 200 ? generateID.AutoNumber : string.Empty;
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
                transNo = generateID.metaData.isSucces && generateID.metaData.code == 200 ? generateID.AutoNumber : string.Empty;
                return transNo;
            }
            catch (Exception e)
            {
                await MsgModel.MsgNotification($"Error: {e.Message}");
                return null;
            }
        }

        public static async Task<string> GetReportNo()
        {
            try
            {
                string reportNo = string.Empty;
                string reportID;
                var userID = SessionModel.GetUserID(App.Session);
                var accessName = SessionModel.GetUserAccess(App.Session);

                switch (accessName)
                {
                    case "Admin":
                        reportID = string.Concat(userID, accessName.Substring(0, 3));
                        break;

                    case "User":
                        {
                            var accessStartSplit = accessName.Substring(0, 2);
                            var accessEndSplit = accessName.Substring(accessName.Length - 1);
                            var access = string.Concat(accessStartSplit, accessEndSplit);
                            reportID = string.Concat(userID, access);
                            break;
                        }

                    default:
                        //Temporary Jika Akses User Bukan Admin Atau User
                        reportID = string.Concat(userID, "TEM");
                        break;
                }

                var generateID = await NewReportNo.GetNewReportNo(reportID);
                reportNo = generateID.metaData.isSucces && generateID.metaData.code == 200 ? generateID.AutoNumber : string.Empty;
                return reportNo;
            }
            catch (Exception e)
            {
                await MsgModel.MsgNotification($"Error: {e.Message}");
                return null;
            }
        }

        public static async Task<string> GetWishlistID(string wishlistType)
        {
            try
            {
                string wishlistID = string.Empty;
                var generateID = await GetNewWishlistID.GetNewWishlistIDNo(wishlistType);
                wishlistID = !string.IsNullOrEmpty(generateID) ? generateID : string.Empty;
                return wishlistID;
            }
            catch (Exception e)
            {
                await MsgModel.MsgNotification($"Error: {e.Message}");
                return null;
            }
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

    public static class Calculator
    {
        public static double Calculate(double value1, double value2, string mathOperator)
        {
            double result = 0;

            switch (mathOperator)
            {
                case "÷":
                    result = value1 / value2;
                    break;
                case "×":
                    result = value1 * value2;
                    break;
                case "+":
                    result = value1 + value2;
                    break;
                case "-":
                    result = value1 - value2;
                    break;
            }
            return result;
        }

        public static string TrimmedString(double value, string decimalFormat)
        {
            string strValue = value.ToString(decimalFormat);

            if (strValue.Contains("."))
            {
                strValue = strValue.TrimEnd('0');

                if (strValue.EndsWith("."))
                    strValue = strValue.TrimEnd('.');
            }

            return strValue;
        }
    }
}
