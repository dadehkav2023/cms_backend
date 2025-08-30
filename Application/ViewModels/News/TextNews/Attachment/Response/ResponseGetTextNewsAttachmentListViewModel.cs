using System.Collections.Generic;
using Application.Utilities;
using Common.Enum;
using Common.EnumList;

namespace Application.ViewModels.News.TextNews.Attachment.Response
{
    public class ResponseGetTextNewsAttachmentListViewModel
    {
        public List<ResponseGetTextNewsAttachmentViewModel> NewsAttachmentList { get; set; }
    }
    
    public class ResponseGetTextNewsAttachmentViewModel
    {
        public int Id { get; set; }
        public string AttachmentFile { get; set; }
        public string Title { get; set; }
        public FileTypeEnum FileType { get; set; }

        public string FileTypeText => FileType.GetDescription();
    }
    
}