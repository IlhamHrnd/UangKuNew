using Newtonsoft.Json;
using UangKu.Model.Base;

namespace UangKu.Model.Response.Picture
{
    public class UserPicture
    {
        public class Datum
        {
            [JsonProperty("pictureID")]
            public string pictureID { get; set; }

            [JsonProperty("picture")]
            public string picture { get; set; }

            [JsonProperty("pictureName")]
            public string pictureName { get; set; }

            [JsonProperty("pictureFormat")]
            public string pictureFormat { get; set; }

            [JsonProperty("personID")]
            public string personID { get; set; }

            [JsonProperty("isDeleted")]
            public bool? isDeleted { get; set; }

            [JsonProperty("createdByUserID")]
            public string createdByUserID { get; set; }

            [JsonProperty("createdDateTime")]
            public DateTime? createdDateTime { get; set; }

            [JsonProperty("lastUpdateDateTime")]
            public DateTime? lastUpdateDateTime { get; set; }

            [JsonProperty("lastUpdateByUserID")]
            public string lastUpdateByUserID { get; set; }
            public ImageSource source { get; set; }
            public string contenttype { get; set; }
        }

        public class UserPictureRoot : IResponse
        {
            [JsonProperty("pageNumber")]
            public int? pageNumber { get; set; }

            [JsonProperty("pageSize")]
            public int? pageSize { get; set; }

            [JsonProperty("totalPages")]
            public int? totalPages { get; set; }

            [JsonProperty("totalRecords")]
            public int? totalRecords { get; set; }

            [JsonProperty("prevPageLink")]
            public object prevPageLink { get; set; }

            [JsonProperty("nextPageLink")]
            public object nextPageLink { get; set; }

            [JsonProperty("data")]
            public List<Datum> data { get; set; }

            [JsonProperty("succeeded")]
            public bool? succeeded { get; set; }

            [JsonProperty("errors")]
            public object errors { get; set; }

            [JsonProperty("message")]
            public object message { get; set; }
            public MetaData metaData { get; set; }
        }
    }

    public class UserPictureTwo
    {
        //List Kedua Yang Di Copy Dari Class UserPicture
        //Untuk Keperluan Compare Item List
        public class Datum
        {
            [JsonProperty("pictureID")]
            public string pictureID { get; set; }

            [JsonProperty("isDeleted")]
            public bool? isDeleted { get; set; }
        }
    }

    public class DifferentUserPicture
    {
        //List Untuk Menampung Data Yang Berbeda Saat Proses Compare Pada UserGalleryVM
        public class Datum
        {
            public string pictureID { get; set; }
            public bool? isDeleted { get; set; }
        }
    }
}
