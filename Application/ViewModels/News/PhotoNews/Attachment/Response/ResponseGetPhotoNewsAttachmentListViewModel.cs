using System.Collections.Generic;
using Application.Utilities;
using Common.Enum;

namespace Application.ViewModels.News.PhotoNews.Attachment.Response
{
    public class ResponseGetPhotoNewsAttachmentListViewModel
    {
        public List<ResponseGetPhotoNewsAttachmentViewModel> NewsAttachmentList { get; set; }
    }
    
    public class ResponseGetPhotoNewsAttachmentViewModel
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string Title { get; set; }
    }
    
}