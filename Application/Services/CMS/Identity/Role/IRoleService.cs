using System.Collections.Generic;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.ViewModels.Accounting.Role.Response;
using Application.ViewModels.CMS.Identity.Request;

namespace Application.Services.CMS.Identity.Role
{
    public interface IRoleService
    {
        Task<IBusinessLogicResult<bool>> SetRoleToUser(RequestAddRolesToUserViewModel requestAddRolesToUserViewModel);
        Task<IBusinessLogicResult<bool>> RemoveRoleFromUser(RequestRemoveRolesFromUserViewModel requestRemoveRolesFromUserViewModel);
        Task<IBusinessLogicResult<List<ResponseRoleViewModel>>> GetAllRoles(int? userId);
        Task<IBusinessLogicResult<List<ResponseRoleViewModel>>> GetRolesOfUser(int userId);
    }
}