using Application.ViewModels.Public;

namespace Application.ViewModels.Statement.Request
{
    public class RequestGetStatementViewModel : RequestGetListViewModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public bool LoadPublishStatements { get; set; } = true;

    }
}