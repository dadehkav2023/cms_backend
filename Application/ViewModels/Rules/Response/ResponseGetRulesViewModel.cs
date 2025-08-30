using System.Collections.Generic;
using Application.ViewModels.Public;
using Application.ViewModels.Rules.Attachment.Response;

namespace Application.ViewModels.Rules.Response
{
    public class ResponseGetRulesViewModel
    {
        public int Id { get; set; }
        public IEnumerable<ResponseGetRulesAttachmentViewModel> RulesAttachments { get; set; }
        public string Description { get; set; }
    }
}