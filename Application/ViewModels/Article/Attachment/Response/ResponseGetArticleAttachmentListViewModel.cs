using System.Collections.Generic;
using Application.Utilities;
using Common.Enum;
using Common.EnumList;

namespace Application.ViewModels.Article.Attachment.Response
{
    public class ResponseGetArticleAttachmentListViewModel
    {
        public List<ResponseGetArticleAttachmentViewModel> ArticleAttachmentList { get; set; }
    }
    
    public class ResponseGetArticleAttachmentViewModel
    {
        public int Id { get; set; }
        public string AttachmentFile { get; set; }
        public string Title { get; set; }
        public FileTypeEnum FileType { get; set; }

        public string FileTypeText => FileType.GetDescription();
    }
    
}