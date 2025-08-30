using System.Collections.Generic;
using Application.Utilities;
using Common.Enum;
using Common.EnumList;

namespace Application.ViewModels.Statement.Attachment.Response
{
    public class ResponseGetStatementAttachmentListViewModel
    {
        public List<ResponseGetStatementAttachmentViewModel> StatementAttachmentList { get; set; }
    }
    
    public class ResponseGetStatementAttachmentViewModel
    {
        public int Id { get; set; }
        public string AttachmentFile { get; set; }
        public string Title { get; set; }
        public FileTypeEnum FileType { get; set; }

        public string FileTypeText => FileType.GetDescription();
    }
    
}