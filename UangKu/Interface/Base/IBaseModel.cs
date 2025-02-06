using System.ComponentModel;
using UangKu.Model.Base;

namespace UangKu.Interface.Base
{
    public interface IBaseModel
    {
        bool IsBusy { get; set; }
        bool IsProgram { get; set; }
        bool IsProgramAddAble { get; set; }
        bool IsProgramApprovalAble { get; set; }
        bool IsProgramDeleteAble { get; set; }
        bool IsProgramEditAble { get; set; }
        bool IsProgramUnApprovalAble { get; set; }
        bool IsProgramUnVoidAble { get; set; }
        bool IsProgramViewAble { get; set; }
        bool IsProgramVoidAble { get; set; }
        bool IsUsedBySystem { get; set; }
        bool IsVisible { get; set; }
        string Title { get; set; }
        int TotalPages { get; set; }
        int TotalRecords { get; set; }
        string UserID { get; set; }
        string Mode { get; set; }
        int PageNumber { get; set; }
        int PageSize { get; set; }
        string PrevPageLink { get; set; }
        string NextPageLink { get; set; }
        DateTime EndDate { get; set; }
        DateTime StartDate { get; set; }
        NetworkModel Network { get; set; }
        void AppProgram(string programID);

        event PropertyChangedEventHandler PropertyChanged;
    }
}
