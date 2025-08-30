using System.Collections.Generic;
using Application.Utilities;
using Common.Enum;

namespace Application.ViewModels.News.VideoNews.Attachment.Response
{
    public class ResponseGetVideoNewsAttachmentListViewModel
    {
        public List<ResponseGetVideoNewsAttachmentViewModel> NewsAttachmentList { get; set; }
    }
    
    public class ResponseGetVideoNewsAttachmentViewModel
    {
        public int Id { get; set; }
        public string VideoPath { get; set; }
        public string Title { get; set; }
    }
    
}