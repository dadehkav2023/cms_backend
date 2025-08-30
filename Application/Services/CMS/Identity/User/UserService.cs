using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.BusinessLogic.Message;
using Application.Interfaces.IRepositories;
using Application.Services.Messages;
using Application.ViewModels.Accounting;
using Application.ViewModels.Accounting.ForgetPass.ByEmail;
using Application.ViewModels.Accounting.ForgetPass.ByPhone;
using Application.ViewModels.Accounting.User.Request;
using Application.ViewModels.Accounting.User.Response;
using Application.ViewModels.ApiImageUploader;
using Application.ViewModels.CMS.Identity.User;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Enum.Message;
using Common.Enum.User;
using Common.HashAlgoritm;
using Domain.Entities.Identity.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.CMS.Identity.User
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private UserManager<Domain.Entities.Identity.User.User> _userManager;
        private RoleManager<Domain.Entities.Identity.Role.Role> _roleManager;
        private SignInManager<Domain.Entities.Identity.User.User> _signInManager;
        private readonly IRepository<UserApiToken> _UserApiToken;
        private readonly IConfiguration _configuration;
        private readonly IRepository<UserReal> _userRealRepository;
        private readonly IRepository<UserLegal> _userLegalRepository;
        private readonly IRepository<UserPotential> _userPotentialRepository;
        private readonly IMessage _smsSender;
        private readonly IMessage _EmailSender;
        private readonly ILogger<UserService> _logger;
        private readonly IOptions<EmailConfigurationViewModel> optionsEmailConfiguration;

        public UserService(IMapper mapper,
                            UserManager<Domain.Entities.Identity.User.User> userManager,
                            RoleManager<Domain.Entities.Identity.Role.Role> roleManager,
                            SignInManager<Domain.Entities.Identity.User.User> signInManager,
                            IUnitOfWorkApplication context,
                            IConfiguration configuration,
                            ILogger<UserService> logger,
                            ILogger<SMS> loggerSMS,
                            ILogger<Email> loggerEmail)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _UserApiToken = context.GetRepository<UserApiToken>();
            _userRealRepository = context.GetRepository<UserReal>();
            _userLegalRepository = context.GetRepository<UserLegal>();
            _userPotentialRepository = context.GetRepository<UserPotential>();
            _configuration = configuration;
            _smsSender = new SMS(loggerSMS, configuration);
            _EmailSender = new Email(loggerEmail, configuration, optionsEmailConfiguration);
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IBusinessLogicResult<ResponseGetUserListViewModel>> GetUser(RequestGetUserListViewModel requestGetUserListViewModel)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var users = _userManager.Users;
                if (!string.IsNullOrEmpty(requestGetUserListViewModel.UserName))
                {
                    users = users.Where(x => x.UserName == requestGetUserListViewModel.UserName);
                }

                if (!string.IsNullOrEmpty(requestGetUserListViewModel.FirstName))
                {
                    users = users.Where(x => x.FirstName.Contains(requestGetUserListViewModel.FirstName));
                }

                if (!string.IsNullOrEmpty(requestGetUserListViewModel.LastName))
                {
                    users = users.Where(x => x.LastName.Contains(requestGetUserListViewModel.LastName));
                }

                if (requestGetUserListViewModel.RoleId != null)
                {

                }

                var userList = users
                    .ProjectTo<ResponseGetUserViewModel>(_mapper.ConfigurationProvider)
                    .Skip((requestGetUserListViewModel.Page - 1) * requestGetUserListViewModel.PageSize)
                    .Take(requestGetUserListViewModel.PageSize);


                var result = new ResponseGetUserListViewModel
                {
                    Count = userList.Count(),
                    CurrentPage = requestGetUserListViewModel.Page,
                    List = userList.ToList(),
                    TotalCount = users.Count()
                };

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<ResponseGetUserListViewModel>(succeeded: true, result: result,
                    messages: messages);
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<ResponseGetUserListViewModel>(succeeded: false, result: null,
                    messages: messages, exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<TokenViewModel>> LoginUser(LoginViewModel login)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var finduser = _userManager.FindByNameAsync(login.UserName).Result;
                if (finduser == null)
                {
                    return null;
                }
                await _signInManager.SignOutAsync();
                var ResultSignIn = _signInManager.PasswordSignInAsync(finduser, login.PassWord, login.IsPersistent, false).Result;
                if (ResultSignIn.Succeeded)
                {
                    var generateToken = GenerateToken(finduser);
                    messages.Add(new BusinessLogicMessage(MessageType.Info, MessageId.Success));
                    return new BusinessLogicResult<TokenViewModel>(succeeded: true, result: generateToken, messages: messages);
                }
                else
                {
                    messages.Add(new BusinessLogicMessage(MessageType.Warning, MessageId.NotExistAccountUser));
                    return new BusinessLogicResult<TokenViewModel>(succeeded: false, result: null, messages: messages);
                }
            }
            catch (Exception exception)
            {
                messages.Add(new BusinessLogicMessage(MessageType.Error, MessageId.Exception));
                return new BusinessLogicResult<TokenViewModel>(succeeded: false, result: null, messages: messages);
            }
        }

        private TokenViewModel GenerateToken(Domain.Entities.Identity.User.User appUserReceive)
        {

            var appUser = _userManager.Users.SingleOrDefault(p => p.Id == appUserReceive.Id);
            // Init ClaimsIdentity
            var claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()));
            claimsIdentity.AddClaim(new Claim("UserName", appUser.UserName));


            //Get UserClaims
            var userClaims = _userManager.GetClaimsAsync(appUser).Result;
            claimsIdentity.AddClaims(userClaims);

            //Get UserRoles
            var userRoles = _userManager.GetRolesAsync(appUser).Result;
            claimsIdentity.AddClaims(userRoles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));
            // ClaimsIdentity.DefaultRoleClaimType & ClaimTypes.Role is the same

            // Get RoleClaims
            foreach (var userRole in userRoles)
            {
                var role = _roleManager.FindByNameAsync(userRole).Result;
                var roleClaims = _roleManager.GetClaimsAsync(role).Result;
                claimsIdentity.AddClaims(roleClaims);
            }

            // Generate access token
            var jwtToken = GenerateJwtToken(claimsIdentity);

            string refreshToken = Guid.NewGuid().ToString();

            _UserApiToken.Add(new UserApiToken()
            {
                TokenHash = SecurityHelper.GetSHA256Hash(jwtToken),
                TokenExp = DateTime.Now.AddMinutes(int.Parse(_configuration["JWtConfig:expires"])),
                RefreshTokenHash = SecurityHelper.GetSHA256Hash(refreshToken),
                RefreshTokenExp = DateTime.Now.AddDays(30),
                // User = appUser,
                // UserId = appUser.Id.ToString(),
                PhoneNumber = appUser.PhoneNumber,
            });

            return new TokenViewModel(jwtToken, refreshToken);

        }

        private string GenerateJwtToken(ClaimsIdentity claimsIdentity)
        {
            string key = _configuration["JWtConfig:Key"];
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenexp = DateTime.Now.AddMinutes(int.Parse(_configuration["JWtConfig:expires"]));

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWtConfig:issuer"],
                Audience = _configuration["JWtConfig:audience"],
                Subject = claimsIdentity,
                NotBefore = DateTime.Now,
                Expires = tokenexp,
                SigningCredentials = credentials,
            });

            return tokenHandler.WriteToken(token);

        }

        public async Task<IBusinessLogicResult<bool>> UserPotentialSaveAsync(UserPotentialInViewModel model)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                bool isUserExisted = false;
                bool isCellphoneExisted = false;
                UserPotential userPotential;
                var UserName = "";
                try
                {
                    if (model.UserType == UserTypeEnum.Real) UserName = model.NationalCode;
                    else UserName = model.NationalId;

                    var FindUser = await _userManager.FindByNameAsync(UserName);
                    if (FindUser != null)
                        isUserExisted = true;

                    if (isUserExisted)
                    {
                        _logger.LogWarning($"User already existed. in UserPotentialSaveAsync.");
                        messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.UserInfoDoublicat));
                        return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                    }                   

                    userPotential = _userPotentialRepository.DeferredWhere(usr => usr.Cellphone == model.Cellphone).OrderByDescending(x => x.Id).FirstOrDefault();
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "Error while checking username existed on users.");
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.InternalError, exception.HResult.ToString()));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages, exception: exception);
                }

                var user = _mapper.Map<UserPotential>(model);
                //generate Token
                Random generator = new Random();
                user.VerificationCode = generator.Next(100000, 999999).ToString();
                user.SecurityStamp = Guid.NewGuid().ToString();
                user.SecurityStampExpirationDateTime = DateTime.Now.AddMinutes(5);
                user.IsConfirmed = false;

                if (user.UserType == UserTypeEnum.Real) user.NationalId = null;
                else user.NationalCode = null;

                try
                {
                    if (userPotential == null || (userPotential != null && userPotential.SecurityStampExpirationDateTime < DateTime.Now))
                    {
                        //send sms service
                        var smsResult = _smsSender.SendByPattern(SmsPatternEnum.Register, model.Cellphone, user.VerificationCode);
                        // var smsResult = _smsSender.SendSms(model.Cellphone, SmsMessagesEnum.Simple, user.VerificationCode);
                        var userResult = await _userPotentialRepository.AddAsync(user);
                        _logger.LogInformation($"User potential Added successfully.");
                        messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.UserSuccessfullyAdded));
                        return new BusinessLogicResult<bool>(succeeded: true, result: true, messages: messages);
                    }
                    else
                    {
                        _logger.LogInformation($"Expiration Date Time not over yet");
                        messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.ExpirationDateTimeNotOver));
                        return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages);
                    }
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "Error while adding user potential to database.");
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.InternalError, exception.HResult.ToString()));
                    return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages, exception: exception);
                }
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception, "Error while adding new user potential.");
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<bool>(succeeded: false, result: false, messages: messages, exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<string>> UserPotentialPhoneConfirmAsync(UserPotentialPhoneConfirmViewModel model)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var user = _userPotentialRepository.DeferredWhere(usr => usr.Cellphone == model.Cellphone).OrderByDescending(x => x.Id).FirstOrDefault();
                if (user == null)
                {
                    _logger.LogInformation($"User not found. in UserPhoneConfirmAsync.");
                    messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.PhoneNotFind));
                    return new BusinessLogicResult<string>(succeeded: true, result: null, messages: messages);
                }
                if (user.VerificationCode == model.VerificationCode)
                {
                    //update IsConFirmed
                    user.IsConfirmed = true;
                    await _userPotentialRepository.UpdateAsync(user, true);
                    _logger.LogInformation($"User Phone Confirmed successfully.");
                    messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.VerificationCodeTrue));
                    return new BusinessLogicResult<string>(succeeded: true, result: user.SecurityStamp, messages: messages);
                }
                else
                {
                    _logger.LogError("User Verification Code Is Not True");
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.VerificationCodeNotTrue));
                    return new BusinessLogicResult<string>(succeeded: false, result: null, messages: messages);
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error while adding verifying Phone.");
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.InternalError, exception.HResult.ToString()));
                return new BusinessLogicResult<string>(succeeded: false, result: null, messages: messages, exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<int>> RegisterUserRealPotential(RegisterUserRealViewModel model)
        {
            var messages = new List<BusinessLogicMessage>();
            UserPotential userPotential;
            try
            {
                try
                {
                    userPotential = _userPotentialRepository.DeferredWhere(usr => usr.SecurityStamp == model.SecurityStamp).OrderByDescending(x => x.Id).FirstOrDefault();
                    if (userPotential == null || !userPotential.IsConfirmed)
                    {
                        _logger.LogWarning($"User not Confirmed.");
                        messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.PhoneNotVerifyed));
                        return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages);
                    }
                    if (userPotential.SecurityStampExpirationDateTime < DateTime.Now)
                    {
                        _logger.LogWarning($"Verification code expired.");
                        messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.VerificationCodeNotTrue));
                        return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages);
                    }
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "Error while checking username existed on register.");
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.InternalError, exception.HResult.ToString()));
                    return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages, exception: exception);
                }


                bool isUserNameExisted;
                bool isEmailExisted;
                try
                {
                    var findUser = await _userManager.FindByNameAsync(model.Username);
                    isUserNameExisted = findUser != null ? true : false;
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "Error while checking username existed on RegisterUserRealAsync.");
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.InternalError, exception.HResult.ToString()));
                    return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages, exception: exception);
                }
                if (isUserNameExisted)
                {
                    _logger.LogWarning($"Unable to register because username {model.Username} already existed.");
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.UserNameAlreadyExisted, viewMessagePlaceHolders: model.Username));
                    return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages);
                }


                try
                {
                    var user = new Domain.Entities.Identity.User.User();
                    user.UserName = model.Username;
                    user.PhoneNumber = userPotential.Cellphone;
                    user.PhoneNumberConfirmed = true;
                    user.Email = model.Email;
                    user.IsActive = true;
                    var _userCreateIdentity = _userManager.CreateAsync(user, model.Password).Result;
                    var _findUser = _userManager.FindByNameAsync(user.UserName).Result;

                    UserReal userReal = new UserReal();
                    userReal.IdentityUserId = _findUser.Id;
                    userReal.Name = model.Name;
                    userReal.LastName = model.LastName;
                    var addUserReal = await _userRealRepository.AddAsync(userReal);

                    _logger.LogInformation($"User by username={model.Username} Added successfully.");
                    messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.UserSuccessfullyAdded));
                    return new BusinessLogicResult<int>(succeeded: true, result: addUserReal.Id, messages: messages);
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "Error while adding user to database.");
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.InternalError, exception.HResult.ToString()));
                    return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages, exception: exception);
                }
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception, "Error while adding new user.");
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages, exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<int>> RegisterUserLegalPotential(RegisterUserLegalViewModel model)
        {
            var messages = new List<BusinessLogicMessage>();
            UserPotential userPotential;
            try
            {
                try
                {
                    userPotential = _userPotentialRepository.DeferredWhere(usr => usr.SecurityStamp == model.SecurityStamp).OrderByDescending(x => x.Id).FirstOrDefault();
                    if (userPotential == null || !userPotential.IsConfirmed)
                    {
                        _logger.LogWarning($"User not Confirmed.");
                        messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.PhoneNotVerifyed));
                        return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages);
                    }
                    if (userPotential.SecurityStampExpirationDateTime < DateTime.Now)
                    {
                        _logger.LogWarning($"Verification code expired.");
                        messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.VerificationCodeNotTrue));
                        return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages);
                    }
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "Error while checking username existed on register.");
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.InternalError, exception.HResult.ToString()));
                    return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages, exception: exception);
                }


                bool isUserNameExisted;
                bool isEmailExisted;
                try
                {
                    var findUser = await _userManager.FindByNameAsync(model.Username);
                    isUserNameExisted = findUser != null ? true : false;
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "Error while checking username existed on RegisterUserRealAsync.");
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.InternalError, exception.HResult.ToString()));
                    return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages, exception: exception);
                }
                if (isUserNameExisted)
                {
                    _logger.LogWarning($"Unable to register because username {model.Username} already existed.");
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.UserNameAlreadyExisted, viewMessagePlaceHolders: model.Username));
                    return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages);
                }


                try
                {
                    var user = new Domain.Entities.Identity.User.User();
                    user.UserName = model.Username;
                    user.PhoneNumber = userPotential.Cellphone;
                    user.PhoneNumberConfirmed = true;
                    user.Email = model.Email;
                    user.IsActive = true;
                    var _userCreateIdentity = _userManager.CreateAsync(user, model.Password).Result;
                    var _findUser = _userManager.FindByNameAsync(user.UserName).Result;

                    UserLegal userLegal = new UserLegal();
                    userLegal.IdentityUserId = _findUser.Id;
                    userLegal.Name = model.Name;
                    var addUserReal = await _userLegalRepository.AddAsync(userLegal);

                    _logger.LogInformation($"User by username={model.Username} Added successfully.");
                    messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.UserSuccessfullyAdded));
                    return new BusinessLogicResult<int>(succeeded: true, result: addUserReal.Id, messages: messages);
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "Error while adding user to database.");
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.InternalError, exception.HResult.ToString()));
                    return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages, exception: exception);
                }
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception, "Error while adding new user.");
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages, exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<int>> RegisterUserReal(RegisterUserRealViewModel model)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                bool isUserNameExisted;
                bool isEmailExisted;
                try
                {
                    var findUser = await _userManager.FindByNameAsync(model.Username);
                    isUserNameExisted = findUser != null ? true : false;
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "Error while checking username existed on RegisterUserRealAsync.");
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.InternalError, exception.HResult.ToString()));
                    return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages, exception: exception);
                }
                if (isUserNameExisted)
                {
                    _logger.LogWarning($"Unable to register because username {model.Username} already existed.");
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.UserNameAlreadyExisted, viewMessagePlaceHolders: model.Username));
                    return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages);
                }
                try
                {
                    var user = new Domain.Entities.Identity.User.User();
                    user.UserName = model.Username;
                    user.Email = model.Email;
                    user.IsActive = true;
                    var _userCreateIdentity = _userManager.CreateAsync(user, model.Password).Result;
                    var _findUser = _userManager.FindByNameAsync(user.UserName).Result;

                    UserReal userReal = new UserReal();
                    userReal.IdentityUserId = _findUser.Id;
                    userReal.Name = model.Name;
                    var addUserReal = await _userRealRepository.AddAsync(userReal);

                    _logger.LogInformation($"User by username={model.Username} Added successfully.");
                    messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.UserSuccessfullyAdded));
                    return new BusinessLogicResult<int>(succeeded: true, result: addUserReal.Id, messages: messages);
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "Error while adding user to database.");
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.InternalError, exception.HResult.ToString()));
                    return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages, exception: exception);
                }
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception, "Error while adding new user.");
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages, exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<int>> RegisterUserLegal(RegisterUserLegalViewModel model)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                bool isUserNameExisted;
                bool isEmailExisted;
                try
                {
                    var findUser = await _userManager.FindByNameAsync(model.Username);
                    isUserNameExisted = findUser != null ? true : false;
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "Error while checking username existed on RegisterUserRealAsync.");
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.InternalError, exception.HResult.ToString()));
                    return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages, exception: exception);
                }
                if (isUserNameExisted)
                {
                    _logger.LogWarning($"Unable to register because username {model.Username} already existed.");
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.UserNameAlreadyExisted, viewMessagePlaceHolders: model.Username));
                    return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages);
                }
                try
                {
                    var user = new Domain.Entities.Identity.User.User();
                    user.UserName = model.Username;
                    user.Email = model.Email;
                    user.IsActive = true;
                    var _userCreateIdentity = _userManager.CreateAsync(user, model.Password).Result;
                    var _findUser = _userManager.FindByNameAsync(user.UserName).Result;

                    UserLegal userLegal = new UserLegal();
                    userLegal.IdentityUserId = _findUser.Id;
                    userLegal.Name = model.Name;
                    var addUserReal = await _userLegalRepository.AddAsync(userLegal);

                    _logger.LogInformation($"User by username={model.Username} Added successfully.");
                    messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.UserSuccessfullyAdded));
                    return new BusinessLogicResult<int>(succeeded: true, result: addUserReal.Id, messages: messages);
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "Error while adding user to database.");
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.InternalError, exception.HResult.ToString()));
                    return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages, exception: exception);
                }
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception, "Error while adding new user.");
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages, exception: exception);
            }
        }
        
        //********************* Phone *****************************
        public async Task<IBusinessLogicResult<int>> ForgotPasswordByPhoneAsync(PhoneConfirmViewmodel model)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var _findUser = _userManager.Users.Where(x => x.PhoneNumber == model.PhoneNumber).FirstOrDefault();
                if (_findUser == null)
                {
                    _logger.LogWarning($"Unable to register because PhoneNumber {model.PhoneNumber} Not existed.");
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.PhoneNotExist, viewMessagePlaceHolders: model.PhoneNumber));
                    return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages);
                }

                var _VerificationCode = _userManager.GenerateChangePhoneNumberTokenAsync(_findUser, model.PhoneNumber).Result;
                var smsResult = _smsSender.SendByPattern(SmsPatternEnum.Register, model.PhoneNumber, _VerificationCode);


                _logger.LogWarning($"Send Message To PhoneNumber: {model.PhoneNumber} is succeeded.");
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.SendAcceptCodeToPhonenumber, viewMessagePlaceHolders: model.PhoneNumber));
                return new BusinessLogicResult<int>(succeeded: true, result: 1, messages: messages);

            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception, "Error while adding new user.");
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages, exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<int>> ResetPasswordByPhoneNumber(ResetPasswordByPhonenumberViewModel model)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var _findUser = _userManager.Users.Where(x => x.PhoneNumber == model.Cellphone).FirstOrDefault();
                if (_findUser == null)
                {
                    _logger.LogWarning($"Unable to register because PhoneNumber {model.Cellphone} Not existed.");
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.PhoneNotExist, viewMessagePlaceHolders: model.Cellphone));
                    return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages);
                }

                var _VerificationCode = _userManager.ResetPasswordAsync(_findUser, model.VerificationCode, model.Password).Result;

                if (!_VerificationCode.Succeeded)
                {
                    _logger.LogWarning($"VerificationCode From : {model.Cellphone} is Not Accept.");
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.VerificationCodeNotTrue));
                    return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages);
                }
               
                _logger.LogWarning($"Reset Pass is Success.");
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.VerificationCodeTrue));
                return new BusinessLogicResult<int>(succeeded: true, result: _findUser.Id, messages: messages);

            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception, "Error while adding new user.");
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages, exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<int>> UserPhoneConfirmAsync(UserPhoneConfirmViewModel model)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var _findUser = _userManager.FindByNameAsync(model.UserName).Result;
                if (_findUser == null)
                {
                    _logger.LogWarning($"Unable to register because PhoneNumber {model.Cellphone} Not existed.");
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.PhoneNotExist, viewMessagePlaceHolders: model.Cellphone));
                    return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages);
                }

                var _VerificationCode = _userManager.VerifyChangePhoneNumberTokenAsync(_findUser, model.VerificationCode, model.Cellphone).Result;
                
                if(!_VerificationCode)
                {
                    _logger.LogWarning($"VerificationCode From : {model.Cellphone} is Not Accept.");
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.VerificationCodeNotTrue));
                    return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages);
                }

                _logger.LogWarning($"VerificationCode From : {model.Cellphone} is Accept.");
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.VerificationCodeTrue));
                return new BusinessLogicResult<int>(succeeded: true, result: _findUser.Id, messages: messages);

            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception, "Error while adding new user.");
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages, exception: exception);
            }
        }
        //
        //********************* Email *****************************
        public async Task<IBusinessLogicResult<int>> ForgotPasswordByEmailAsync(ForgetPasswordByEmailViewmodel model)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    _logger.LogWarning($"Unable to Forgot Password By Email {model.Email} Not existed.");
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.PhoneNotExist, viewMessagePlaceHolders: model.Email));
                    return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages);
                }
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var resultSendEmail = _EmailSender.Send(user.Email, "", tokns: token).Result;

                _logger.LogWarning($"Unable to Forgot Password By Email {model.Email} Not existed.");
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.EmailConfirmCode));
                return new BusinessLogicResult<int>(succeeded: true, result: 1, messages: messages);

            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception, "Error while adding new user.");
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages, exception: exception);
            }

          
        }

        public async Task<IBusinessLogicResult<int>> ResetPasswordByEmail(ResetPasswordByEmailViewModel model)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var _findUser = _userManager.FindByEmailAsync(model.Email).Result;
                if (_findUser == null)
                {
                    _logger.LogWarning($"Unable to register because Email {model.Email} Not existed.");
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.EmailNotExist, viewMessagePlaceHolders: model.Email));
                    return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages);
                }

                var _VerificationCode = _userManager.ResetPasswordAsync(_findUser, model.VerificationCode, model.Password).Result;

                if (!_VerificationCode.Succeeded)
                {
                    _logger.LogWarning($"VerificationCode From : {model.Email} is Not Accept.");
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.VerificationCodeNotTrue));
                    return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages);
                }

                _logger.LogWarning($"Reset Pass is Success.");
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.VerificationCodeTrue));
                return new BusinessLogicResult<int>(succeeded: true, result: _findUser.Id, messages: messages);

            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception, "Error while adding new user.");
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages, exception: exception);
            }
        }

        public async Task<IBusinessLogicResult<int>> UserEmailConfirmAsync(UserEmailConfirmViewModel model)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var _findUser = _userManager.FindByNameAsync(model.UserName).Result;
                if (_findUser == null)
                {
                    _logger.LogWarning($"Unable to register because Email {model.Email} Not existed.");
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.EmailNotExist, viewMessagePlaceHolders: model.Email));
                    return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages);
                }

                var _VerificationCode = _userManager.ConfirmEmailAsync(_findUser, model.VerificationCode).Result;

                if (!_VerificationCode.Succeeded)
                {
                    _logger.LogWarning($"VerificationCode From : {model.Email} is Not Accept.");
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.VerificationCodeNotTrue));
                    return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages);
                }

                _logger.LogWarning($"VerificationCode From : {model.Email} is Accept.");
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.VerificationCodeTrue));
                return new BusinessLogicResult<int>(succeeded: true, result: _findUser.Id, messages: messages);

            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception, "Error while adding new user.");
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages, exception: exception);
            }
        }

        //******************* ChangePassword **********************
        public async Task<IBusinessLogicResult<int>> ChangePassword(ChangePassword model)
        {
            var messages = new List<BusinessLogicMessage>();
            try
            {
                var _findUser = _userManager.FindByIdAsync(model.UserId).Result;
                if (_findUser == null)
                {
                    _logger.LogWarning($"Not Find User : {model.UserId}");
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.PhoneNotExist));
                    return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages);
                }
                if(model.NewPassword != model.ConfirmNewPassword)
                {
                    _logger.LogWarning($"Not Find User : {model.UserId}");
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.ComparePassNotEqual));
                    return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages);
                }

                var _VerificationCode = _userManager.ChangePasswordAsync(_findUser, model.CurrentPassword, model.NewPassword).Result;

                if (!_VerificationCode.Succeeded)
                {
                    _logger.LogWarning($"Not Succeded");
                    messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.ErrorInChangePass));
                    return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages);
                }

                _logger.LogWarning($"change Pass is Success");
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.SuccessChangePass));
                return new BusinessLogicResult<int>(succeeded: true, result: _findUser.Id, messages: messages);

            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception, "Error while adding new user.");
                messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
                return new BusinessLogicResult<int>(succeeded: false, result: 0, messages: messages, exception: exception);
            }
        }

        public Task<IBusinessLogicResult<bool>> LogUser(RequestLogUserViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}