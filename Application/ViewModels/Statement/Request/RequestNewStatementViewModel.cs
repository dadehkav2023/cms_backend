using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Application.ViewModels.Statement.Request
{
    public class RequestNewStatementViewModel
    {
        [Required(ErrorMessage = "عنوان را وارد کنید")]
        [MinLength(5, ErrorMessage = "حداقل طول عنوان 5 کاراکتر می باشد")]
        public string Title { get; set; }
        public IFormFile ImagePath { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
       public DateTime? PublishDateTime { get; set; }

        public List<int> CategoriesId { get; set; }

    }
}