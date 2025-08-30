using Common.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Menu.Request
{
    public class MenuComponentRequest{
        public int Id { get; set; } = 0;
        public string Title { get; set; }
        public IFormFile IconPath { get; set; }
        public bool IsActive { get; set; }
        public bool IsRoot { get; set; } = false;
    }
    public class MenuViewModelRequest : MenuComponentRequest
    {
        public List<MenuComponentRequest> menuComponentRequests { get; set; }
    }

    public class MenuItemViewModelRequest : MenuComponentRequest
    {
        public string CKEditorText { get; set; }
        public string Link { get; set; }
        public IFormFile File { get; set; }
        public ServiceTypeEnum? ModularPage { get; set; }
    }

    public class MenuComponentResult
    {
        public int Id { get; set; } = 0;
        public string Title { get; set; }
        public string IconPath { get; set; }
        public bool IsActive { get; set; }
        public MenuItemResult menuItem { get; set; }

        public List<MenuComponentResult> menuComponentResponses { get; set; }

    }

    public class MenuItemResult
    {
        public string CKEditorText { get; set; }
        public string Link { get; set; }
        public string FilePath { get; set; }
        public ServiceTypeEnum? ModularPage { get; set; }
    }

    public class MenuComponentResponse
    {
        public int Id { get; set; } = 0;
        public string Title { get; set; }
        public string IconPath { get; set; }
        public bool IsActive { get; set; }
    }
    public class MenuViewModelResponse : MenuComponentResponse
    {
        public List<MenuComponentResponse> menuComponentResponses { get; set; }
    }

    public class MenuItemViewModelResponse : MenuComponentResponse
    {
        public string CKEditorText { get; set; }
        public string Link { get; set; }
        public string FilePath { get; set; }
        public ServiceTypeEnum? ModularPage { get; set; }
    }

    //public class MenuViewModelResponse
    //{
    //    public int Id { get; set; } = 0;
    //    public string Title { get; set; }
    //    public string IconPath { get; set; }
    //    public bool IsActive { get; set; }
    //    //***********************
    //    public int FatherId { get; set; }
    //    public string CKEditorText { get; set; }
    //    public string Link { get; set; }
    //    public string FilePath { get; set; }
    //    public ServiceTypeEnum? ModularPage { get; set; }

    //    public List<MenuViewModelResponse> submenu { get; set; }

    //}
}
