using System.Collections.Generic;
using Application.Utilities;
using Common.Enum;
using Common.EnumList;

namespace Application.ViewModels.Rules.Attachment.Response
{
    public class ResponseGetRulesAttachmentListViewModel
    {
        public List<ResponseGetRulesAttachmentViewModel> RulesAttachmentList { get; set; }
    }
    
    public class ResponseGetRulesAttachmentViewModel
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public string Title { get; set; }
        public FileTypeEnum FileType { get; set; }

        public string FileTypeText => FileType.GetDescription();
    }
    
}