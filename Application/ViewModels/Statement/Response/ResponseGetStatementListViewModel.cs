using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Application.Utilities;
using Application.ViewModels.Public;
using Application.ViewModels.Statement.Category.Response;

namespace Application.ViewModels.Statement.Response
{
    public class ResponseGetStatementListViewModel : ResponseGetListViewModel
    {
        public List<ResponseGetStatementViewModel> StatementList { get; set; }
    }

    public class ResponseGetStatementViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        [JsonIgnore] public DateTime PublishDateTime { get; set; }
        public string PublishedDateTimeAsJalali => PublishDateTime.ConvertMiladiToJalali();

        public IEnumerable<ResponseGetStatementCategoryViewModel> StatementCategories { get; set; }
    }
}