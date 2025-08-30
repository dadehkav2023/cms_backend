using Application.BusinessLogic;
using Application.ViewModels.CMS.Setting.Request;
using Application.ViewModels.CMS.Setting.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CMS.Setting
{
    public interface ISettingService
    {
        Task<IBusinessLogicResult<bool>> SetSetting(RequestSetSettingViewModel requestSetSettingViewModel, int UserId);
        Task<IBusinessLogicResult<ResponseGetSettingViewModel>> GetSetting(int UserId);
    }
}
