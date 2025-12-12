using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Application.BusinessLogic;
using Application.BusinessLogic.Message;
using Application.DTO.Wallet;
using Application.Interfaces.IRepositories;
using Application.Services.WALLET.Services.Interface;
using Application.Services.WALLET.ViewModels;
using Common.EnumList.Financial;
using Common.EnumList.WALLETEnums;
using Domain.Entities.Financial;
using Domain.Entities.Identity.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RestSharp.Serialization.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Application.Services.WALLET.Services.Concrete;

public class BankService : IBankService
{
    private readonly IRepository<FinancialTransaction> _financialTransactionDbSet;

    private readonly IWalletService _walletService;

    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly UserManager<User> _userManager;
    private readonly string terminalId;
    private readonly string merchantId;
    private readonly string merchantKey;

    private readonly string webReturnUrl;
    private readonly string mobileReturnUrl;
    private readonly string desktopReturnUrl;

    private readonly string failedWebReturnUrl;
    private readonly string failedMobileReturnUrl;
    private readonly string failedDesktopReturnUrl;

    public BankService(IUnitOfWork unitOfWork, IConfiguration configuration, IHttpContextAccessor contextAccessor,
        IWalletService walletService, UserManager<User> userManager)

    {
        _financialTransactionDbSet = unitOfWork.GetRepository<FinancialTransaction>();

        _walletService = walletService;
        _userManager = userManager;

        _configuration = configuration;
        _contextAccessor = contextAccessor;

        terminalId = _configuration.GetSection("BankGateway:TerminalId").Value;
        merchantId = _configuration.GetSection("BankGateway:MerchantId").Value;
        merchantKey = _configuration.GetSection("BankGateway:MerchantKey").Value;

        webReturnUrl = _configuration.GetSection("BankGateway:WebSuccessTransactionRedirectUrl").Value;
        mobileReturnUrl = _configuration.GetSection("BankGateway:MobileSuccessTransactionRedirectUrl").Value;
        desktopReturnUrl = _configuration.GetSection("BankGateway:DesktopSuccessTransactionRedirectUrl").Value;

        failedWebReturnUrl = _configuration.GetSection("BankGateway:WebErrorTransactionRedirectUrl").Value;
        failedMobileReturnUrl = _configuration.GetSection("BankGateway:MobileErrorTransactionRedirectUrl").Value;
        failedDesktopReturnUrl = _configuration.GetSection("BankGateway:DesktopErrorTransactionRedirectUrl").Value;
    }

    public async Task<BusinessLogicResult<GetSepBankTokenViewModel>> GetSepBankToken(long amount)
    {
        var messages = new List<BusinessLogicMessage>();

        try
        {
            string apiUrl = _configuration.GetSection("BankGateway:APIUrl").Value +
                            _configuration.GetSection("BankGateway:GetTokenApiAddress").Value;

            var userName = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Username").Value;
            var userId = _userManager.Users.Where(x => x.UserName == userName).Select(x => x.Id).FirstOrDefault();

            using (var httpClient = new HttpClient())
            {
                #region Create Transaction

                var transaction = new FinancialTransaction()
                {
                    RequestAmount = amount,
                    UserId = userId,
                    Status = FinancialTransactionStatus.Pending,
                    Type = FinancialTransactionTypeEnum.ChargeWallet,
                };
                await _financialTransactionDbSet.AddAsync(transaction);

                #endregion


                var dataBytes =
                    Encoding.UTF8.GetBytes(string.Format("{0};{1};{2}", terminalId, transaction.Id, amount));

                var symmetric = SymmetricAlgorithm.Create("TripleDes");
                symmetric.Mode = CipherMode.ECB;
                symmetric.Padding = PaddingMode.PKCS7;

                var encryptor = symmetric.CreateEncryptor(Convert.FromBase64String(merchantKey), new byte[8]);

                var signData =
                    Convert.ToBase64String(encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length));

                var data = new
                {
                    TerminalId = terminalId,
                    MerchantId = merchantId,
                    Amount = amount,
                    SignData = signData,
                    ReturnUrl = _configuration.GetSection("BankGateway:ReturnUrl").Value,
                    LocalDateTime = DateTime.Now,
                    OrderId = transaction.Id,
                };

                var jsonRequest = JsonSerializer.Serialize(data);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(apiUrl, content);
                response.EnsureSuccessStatusCode();
                var responseJson = await response.Content.ReadAsStringAsync();

                var responseContent =
                    JsonSerializer.Deserialize<ResponseGetTokenDto>(responseJson);
                if (responseContent == null)
                {
                }

                #region Update Transaction

                transaction.Description = responseContent.Description;
                transaction.ResCode = responseContent.ResCode;
                transaction.BankToken = responseContent.Token;


                var resultUpdate = await _financialTransactionDbSet.UpdateAsync(transaction, true);
                if (resultUpdate == null)
                {
                    throw new Exception("failed update");
                }

                #endregion

                var result = new GetSepBankTokenViewModel
                {
                    Token = transaction.BankToken,
                    Description = transaction.Description,
                    ResCode = responseContent.ResCode,
                    ReturnUrl = data.ReturnUrl,
                };


                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<GetSepBankTokenViewModel>(succeeded: true, result: result,
                    messages: messages);
            }
        }
        catch (Exception e)
        {
            messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.Exception));
            return new BusinessLogicResult<GetSepBankTokenViewModel>(succeeded: false, result: null, messages: messages,
                exception: e);
        }
    }

    public async Task<BusinessLogicResult<ShaparakVerifyPaymentResponseViewModel>> VerifyPayment(
        ShaparakVerifyPaymentRequestViewModel model)
    {
        var apiUrl = _configuration.GetSection("BankGateway:APIUrl").Value +
                     _configuration.GetSection("BankGateway:VerifyToken").Value;

        string returnUrl = null;
        string failedReturnUrl = null;

        returnUrl = webReturnUrl;
        failedReturnUrl = failedWebReturnUrl;
        var messages = new List<BusinessLogicMessage>();

        try
        {
            #region FIND TRANSACTION && DETERMINE RETURN URL

            var transaction = await _financialTransactionDbSet
                .DeferredWhere(x => x.Status != FinancialTransactionStatus.Succeeded)
                .FirstOrDefaultAsync(x => x.BankToken == model.Token);
            if (transaction == null)
            {
                throw new Exception("transaction not valid");
            }

            #endregion

            using (var httpClient = new HttpClient())
            {
                var dataBytes = Encoding.UTF8.GetBytes(model.Token);
                var symmetric = SymmetricAlgorithm.Create("TripleDes");
                symmetric.Mode = CipherMode.ECB;
                symmetric.Padding = PaddingMode.PKCS7;
                var encryptor = symmetric.CreateEncryptor(Convert.FromBase64String(merchantKey), new byte[8]);
                var signedData = Convert.ToBase64String(encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length));
                var data = new { Token = model.Token, SignData = signedData };

                var jsonRequest = System.Text.Json.JsonSerializer.Serialize(data);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(apiUrl, content);
                response.EnsureSuccessStatusCode();

                var readResult = await response.Content.ReadAsStringAsync();
                var responseContent = JsonSerializer.Deserialize<VerifyResultData>(readResult);
                if (responseContent == null || responseContent.ResCode == "-1")
                {
                    throw new Exception("response content is null");
                }

                if (responseContent.ResCode == "0")
                {
                    transaction.ResCode = responseContent.ResCode;
                    transaction.Amount = responseContent.Amount;
                    transaction.RefNum = responseContent.RetrivalRefNo;
                    transaction.Status = FinancialTransactionStatus.Succeeded;
                    //todo error when OrderId is null
                    transaction.BankResponseAsJson = JsonSerializer.Serialize(responseContent);
                }
                else
                {
                    transaction.Status = FinancialTransactionStatus.NotSucceeded;
                }

                await _financialTransactionDbSet.UpdateAsync(transaction, false);
                _walletService.ChargeWallet(transaction, true);


                var result = new ShaparakVerifyPaymentResponseViewModel
                {
                    ResCode = transaction.ResCode.ToString(),
                    RefNum = transaction.RefNum,
                    Success = true,
                    ReturnUrl = returnUrl,
                };

                messages.Add(new BusinessLogicMessage(type: MessageType.Info, message: MessageId.Success));
                return new BusinessLogicResult<ShaparakVerifyPaymentResponseViewModel>(succeeded: true, result: result,
                    messages: messages);
            }
        }
        catch (Exception exception)
        {
            messages.Add(new BusinessLogicMessage(type: MessageType.Error, message: MessageId.ErrorOccured));
            return new BusinessLogicResult<ShaparakVerifyPaymentResponseViewModel>(succeeded: false,
                result: new ShaparakVerifyPaymentResponseViewModel
                {
                    ResCode = string.Empty,
                    RefNum = string.Empty,
                    BasketCode = string.Empty,
                    Success = false,
                    ReturnUrl = failedReturnUrl,
                },
                messages: messages);
        }
    }
}