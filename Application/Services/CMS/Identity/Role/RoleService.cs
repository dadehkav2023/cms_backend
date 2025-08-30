using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.BusinessLogic.Message;
using Application.Utilities;
using Application.ViewModels.Accounting.Role.Response;
using Application.ViewModels.CMS.Identity.Request;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Enum;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.CMS.Identity.Role
{
    public class RoleService : IRoleService
    {
        private UserManager<Domain.Entities.Identity.User.User> _userManager;
        private RoleManager<Domain.Entities.Identity.Role.Role> _roleManager;
        private readonly IMapper _mapper;

        public RoleService(IMapper mapper, UserManager<Domain.Entities.Identity.User.User> userManager,
            RoleManager<Domain.Entities.Identity.Role.Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<IBusinessLogicResult<bool>> SetRoleToUser(
            RequestAddRolesToUserViewModel requestAddRolesToUserViewModel)
        {
            var messages = new List<BusinessLogicMessage>();

            try
            {
                var user = _userManager.FindByIdAsync(requestAddRolesToUserViewModel.UserId.ToString()).Result;
                if (user == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.UserIsWrong));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false,
                        messages: messages);
                }

                await _userManager.AddToRolesAsync(user, requestAddRolesToUserViewModel.RolesName);

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<bool>(succeeded: true, result: true,
                    messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<bool>(succeeded: false, result: false,
                    messages: messages, exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<bool>> RemoveRoleFromUser(
            RequestRemoveRolesFromUserViewModel requestRemoveRolesFromUserViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var user = _userManager.FindByIdAsync(requestRemoveRolesFromUserViewModel.UserId.ToString()).Result;
                if (user == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.UserIsWrong));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false,
                        messages: messages);
                }

                await _userManager.RemoveFromRolesAsync(user, requestRemoveRolesFromUserViewModel.RolesName);

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<bool>(succeeded: true, result: true,
                    messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<bool>(succeeded: false, result: false,
                    messages: messages, exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<List<ResponseRoleViewModel>>> GetAllRoles(int? userId)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var roles = _roleManager.Roles;

                if (userId is > 0)
                {
                    var user = _userManager.FindByIdAsync(userId.ToString()).Result;
                    if (user == null)
                    {
                        messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.UserIsWrong));
                        return new BusinessLogicResult<List<ResponseRoleViewModel>>(succeeded: false, result: null,
                            messages: messages);
                    }

                    var rolesNameOfUser = _userManager.GetRolesAsync(user).Result.ToList();
                    roles = roles.Where(r => !rolesNameOfUser.Contains(r.Name));
                }

                var result = roles.ProjectTo<ResponseRoleViewModel>(_mapper.ConfigurationProvider).ToList();

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<List<ResponseRoleViewModel>>(succeeded: true, result: result,
                    messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<List<ResponseRoleViewModel>>(succeeded: false, result: null,
                    messages: messages, exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<List<ResponseRoleViewModel>>> GetRolesOfUser(int userId)
        {
            var messages = new List<BusinessLogicMessage>();

            try
            {
                var user = _userManager.FindByIdAsync(userId.ToString()).Result;
                
                if (userId == 0 || user == null)
                {
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.UserIsWrong));
                    return new BusinessLogicResult<List<ResponseRoleViewModel>>(succeeded: false, result: null,
                        messages: messages);
                }

                var rolesName = _userManager.GetRolesAsync(user).Result;
                var roles = _roleManager.Roles.Where(r => rolesName.AsEnumerable().Contains(r.Name))
                    .ProjectTo<ResponseRoleViewModel>(_mapper.ConfigurationProvider).ToList();

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<List<ResponseRoleViewModel>>(succeeded: true, result: roles,
                    messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<List<ResponseRoleViewModel>>(succeeded: false, result: null,
                    messages: messages, exception: exception);
            }
        }
    }
}